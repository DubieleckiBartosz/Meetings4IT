using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;

namespace Panels.Application.Features.Users.Commands.DeleteUserOpinion;

public class DeleteUserOpinionHandler : ICommandHandler<DeleteUserOpinionCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _currentUser;

    public DeleteUserOpinionHandler(IUserRepository userRepository, ICurrentUser currentUser)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(DeleteUserOpinionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithOpinionsById(request.UserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException($"User not found. [Handler {this.GetType().Name}, " +
                $"Request.UserId {request.UserId}, CurrentUser {_currentUser.UserId}]");
        }

        user.DeleteOpinion(request.OpinionId, _currentUser.UserId);

        _userRepository.Update(user);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}