using Identities.Core.Enums;
using Identities.Core.Helpers;
using Identities.Core.Integration;
using Identities.Core.Integration.Events;
using Identities.Core.Interfaces.Repositories;
using Identities.Core.Interfaces.Services;
using Identities.Core.Models.Entities;
using Identities.Core.Models.Parameters;
using Identities.Core.Options;
using Identities.Core.Responses;
using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Identities.Core.Services;

internal class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IIdentityIntegrationEventService _identityIntegrationEventService;
    private readonly PathOptions _options;

    public UserCommandService(IUserRepository userRepository, IIdentityIntegrationEventService identityIntegrationEventService, IOptions<PathOptions> options)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _identityIntegrationEventService = identityIntegrationEventService ?? throw new ArgumentNullException(nameof(identityIntegrationEventService));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<Response> RegisterUserAsync(RegisterUserParameters parameters)
    {
        var applicationUser = new ApplicationUser()
        {
            UserName = parameters.UserName,
            Email = parameters.Email,
            FirstName = parameters.FirstName,
            LastName = parameters.LastName
        };

        var result = await _userRepository.CreateUserAsync(applicationUser, parameters.Password);
        if (result.Succeeded!)
        {
            var errors = result.ReadResult();
            return Response<List<IdentityErrorResponse>>.Error(errors);
        }

        var resultRole = await _userRepository.UserToRoleAsync(applicationUser, Roles.User);
        if (resultRole.Succeeded!)
        {
            var errors = result.ReadResult();
            return Response<List<IdentityErrorResponse>>.Error(errors);
        }

        var token = await _userRepository.GenerateEmailConfirmationTokenAsync(applicationUser);

        var routeUri = new Uri(string.Concat($"{_options.ClientAddress}", _options.ConfirmUserPath));
        var verificationUri = QueryHelpers.AddQueryString(routeUri.ToString(), "code", token);

        await _identityIntegrationEventService.SaveEventAndPublishAsync(
            new UserRegisteredIntegrationEvent(applicationUser.Email, applicationUser.UserName, verificationUri));

        return Response.Ok();
    }

    public async Task<Response> ResetPasswordUserAsync(ResetPasswordParameters parameters)
    {
        var user = await _userRepository.GetUserByEmailAsync(parameters.Email);
        if (user == null)
        {
            throw new NotFoundException(ErrorMessages.UserNotFound(parameters.Email));
        }

        var resetPassResult = await _userRepository.ResetUserPasswordAsync(user, parameters.Token, parameters.Password);
        if (resetPassResult.Succeeded!)
        {
            var errors = resetPassResult.ReadResult();
            return Response<List<IdentityErrorResponse>>.Error(errors);
        }

        return Response.Ok();
    }

    public async Task<Response> UpdateDataUserAsync(UpdateUserParameters parameters)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> ForgotPasswordAsync(ForgotPasswordParameters parameters)
    {
        var user = await _userRepository.GetUserByEmailAsync(parameters.Email);
        if (user == null)
        {
            throw new NotFoundException(ErrorMessages.UserNotFound(parameters.Email));
        }

        var token = await _userRepository.GeneratePasswordResetTokenAsync(user);
        var link = _options.ClientAddress + _options.ResetPasswordPath;

        await _identityIntegrationEventService.SaveEventAndPublishAsync(
            new UserForgotPasswordIntegrationEvent(user.Email, link, token));

        return Response.Ok();
    }

    public async Task<Response> ConfirmUserAsync(string tokenConfirmation, string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new NotFoundException(ErrorMessages.UserNotFound(email));
        }

        var result = await _userRepository.ConfirmUserAsync(user!, tokenConfirmation);
        if (result.Succeeded!)
        {
            var errors = result.ReadResult();
            return Response<List<IdentityErrorResponse>>.Error(errors);
        }

        return Response.Ok();
    }
}