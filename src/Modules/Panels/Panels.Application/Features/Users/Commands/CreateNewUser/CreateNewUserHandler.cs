using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.Users;
using Serilog;

namespace Panels.Application.Features.Users.Commands.CreateNewUser;

public class CreateNewUserHandler : ICommandHandler<CreateNewUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public CreateNewUserHandler(IUserRepository userRepository, ILogger logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.UserExistsAsync(request.UserId, cancellationToken);
        if (userExists)
        {
            throw new ArgumentException($"User already exists: {request.UserId}");
        }

        var newUser = User.Create(request.UserId, request.Name, request.Email!);

        await _userRepository.AddAsync(newUser, cancellationToken);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        _logger.Information($"User with external id {request.UserId} created");
        return Unit.Value;
    }
}