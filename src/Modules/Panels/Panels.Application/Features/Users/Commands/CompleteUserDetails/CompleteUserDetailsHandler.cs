using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.Http;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.DomainServices.DomainServiceInterfaces;
using Panels.Domain.Users;
using Panels.Domain.Users.Technologies;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Application.Features.Users.Commands.CompleteUserDetails;

public class CompleteUserDetailsHandler : ICommandHandler<CompleteUserDetailsCommand, Response>
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserRepository _userRepository;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IUserDomainService _userDomainService;

    public CompleteUserDetailsHandler(
        ICurrentUser currentUser,
        IUserRepository userRepository,
        ITechnologyRepository technologyRepository,
        IUserDomainService userDomainService)
    {
        _currentUser = currentUser;
        _userRepository = userRepository;
        _technologyRepository = technologyRepository;
        _userDomainService = userDomainService;
    }

    public async Task<Response> Handle(CompleteUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var user = await _userRepository.GetUserByIdentifierAsync(userId);
        if (user == null)
        {
            throw new NotFoundException($"User {userId} not found.");
        }

        var userImage = GetUserImage(request.Image, user);
        var existingTechnologies = await GetExistingTechnologiesWhenUserWantsToAdd(request.Technologies, cancellationToken);

        user = _userDomainService.Complete(user, request.Technologies, userImage, existingTechnologies);

        _userRepository.Update(user);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }

    private UserImage? GetUserImage(IFormFile? image, User user)
    {
        if (image == null)
        {
            return null;
        }

        //Save the file and get the key

        return UserImage.Create(user.Id, "[TODO]");
    }

    private async Task<List<Technology>?> GetExistingTechnologiesWhenUserWantsToAdd(
        List<string>? technologies, CancellationToken cancellationToken)
    {
        if (technologies != null && technologies.Any())
        {
            return await _technologyRepository.GetAllTechnologiesNTAsync(cancellationToken);
        }

        return null;
    }
}