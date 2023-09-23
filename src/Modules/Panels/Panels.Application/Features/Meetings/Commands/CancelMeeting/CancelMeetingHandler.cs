using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;

namespace Panels.Application.Features.Meetings.Commands.CancelMeeting;

public class CancelMeetingHandler : ICommandHandler<CancelMeetingCommand, Response>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly ICurrentUser _currentUser;

    public CancelMeetingHandler(IMeetingRepository meetingRepository, ICurrentUser currentUser)
    {
        _meetingRepository = meetingRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(CancelMeetingCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var meeting = await _meetingRepository.GetMeetingWithInvitationsByIdAsync(request.MeetingId, cancellationToken);
        if (meeting == null || meeting.Organizer.Identifier != userId)
        {
            throw new NotFoundException($"Meeting {request.MeetingId} not found.");
        }

        meeting.Cancel(request.Reason);

        _meetingRepository.UpdateMeeting(meeting);
        await _meetingRepository.UnitOfWork.SaveAsync(cancellationToken);

        return Response.Ok();
    }
}