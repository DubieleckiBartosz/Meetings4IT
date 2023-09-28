using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Application.Features.Users.Commands.AddNewUserOpinion;

public class AddNewUserOpinionHandler : ICommandHandler<AddNewUserOpinionCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _currentUser;

    public AddNewUserOpinionHandler(IUserRepository userRepository, ICurrentUser currentUser)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(AddNewUserOpinionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithOpinionsById(request.UserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException($"User not found. [Handler {this.GetType().Name}, " +
                $"Request.UserId {request.UserId}, CurrentUser {_currentUser.UserId}]");
        }

        var creatorId = _currentUser.UserId;
        var creatorName = _currentUser.UserName;
        Rating? ratingTechnicalSkills = request.RatingTechSkills;
        Rating? ratingSoftSkills = request.RatingSoftSkills;
        Content? content = request.Content;

        user.AddOpinion(creatorId, creatorName,
            ratingTechnicalSkills, ratingSoftSkills, content);

        _userRepository.Update(user);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}