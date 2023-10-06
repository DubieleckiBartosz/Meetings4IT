using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;

namespace Panels.Application.Features.Meetings.Commands.AddMeetingComment;

public class AddMeetingCommentHandler : ICommandHandler<AddMeetingCommentCommand, Response>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly ICurrentUser _currentUser;

    public AddMeetingCommentHandler(IMeetingRepository meetingRepository, ICurrentUser currentUser)
    {
        _meetingRepository = meetingRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(AddMeetingCommentCommand request, CancellationToken cancellationToken)
    {
        var meetingResult = await _meetingRepository.GetMeetingWithCommentsByIdAsync(request.MeetingId, cancellationToken);
        if (meetingResult == null)
        {
            throw new NotFoundException($"Meeting {request.MeetingId} not found.");
        }

        var userId = _currentUser.UserId;
        var userName = _currentUser.UserName;

        meetingResult.AddComment(userId, userName, request.Content!);

        _meetingRepository.UpdateMeeting(meetingResult);
        await _meetingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Ok();
    }
}