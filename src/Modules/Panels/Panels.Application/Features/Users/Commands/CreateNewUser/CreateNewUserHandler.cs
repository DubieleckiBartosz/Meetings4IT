using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.Users;
using Serilog;

namespace Panels.Application.Features.Users.Commands.CreateNewUser;

public class CreateNewUserHandler : ICommandHandler<CreateNewUserCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public CreateNewUserHandler(IUserRepository userRepository, ILogger logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Response> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.UserExistsAsync(request.UserId, cancellationToken);
        if (userExists)
        {
            throw new ArgumentException($"User already exists: {request.UserId}");
        }

        var newUser = User.Create(request.UserId, request.Name, request.Email!, request.City);

        await _userRepository.AddAsync(newUser, cancellationToken);

        //Update all invitations containing this user via stored procedures
        //- this is a potential solution, but we break some best practices

        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        _logger.Information($"User with external id {request.UserId} created");
        return Response.Ok();
    }
}