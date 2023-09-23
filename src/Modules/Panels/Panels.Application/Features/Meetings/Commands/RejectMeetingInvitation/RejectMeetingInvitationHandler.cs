using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;

namespace Panels.Application.Features.Meetings.Commands.RejectMeetingInvitation;

public class RejectMeetingInvitationHandler : ICommandHandler<RejectMeetingInvitationCommand, Response>
{
    private readonly IMeetingRepository _meetingRepository;

    public RejectMeetingInvitationHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<Response> Handle(RejectMeetingInvitationCommand request, CancellationToken cancellationToken)
    {
        var meetingResult = await _meetingRepository.GetMeetingWithInvitationsByIdAsync(request.MeetingId, cancellationToken);
        if (meetingResult == null)
        {
            throw new NotFoundException($"Meeting {request.MeetingId} not found.");
        }

        meetingResult.RejectInvitation(request.InvitationCode);
        _meetingRepository.UpdateMeeting(meetingResult);

        await _meetingRepository.UnitOfWork.SaveAsync(cancellationToken);

        return Response.Ok();
    }
}