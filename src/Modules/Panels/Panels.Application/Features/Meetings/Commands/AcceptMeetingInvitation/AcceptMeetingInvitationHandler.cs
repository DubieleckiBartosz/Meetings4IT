using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Contracts.Repositories;

namespace Panels.Application.Features.Meetings.Commands.AcceptMeetingInvitation;

public class AcceptMeetingInvitationHandler : ICommandHandler<AcceptMeetingInvitationCommand, Response>
{
    private readonly IMeetingRepository _meetingRepository;

    public AcceptMeetingInvitationHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<Response> Handle(AcceptMeetingInvitationCommand request, CancellationToken cancellationToken)
    {
        var meetingResult = await _meetingRepository.GetMeetingWithInvitationsByIdAsync(request.MeetingId, cancellationToken);
        if (meetingResult == null)
        {
            throw new NotFoundException($"Meeting {request.MeetingId} not found.");
        }

        meetingResult.AcceptInvitation(request.InvitationCode);
        _meetingRepository.UpdateMeeting(meetingResult);

        await _meetingRepository.UnitOfWork.SaveAsync(cancellationToken);

        return Response.Ok();
    }
}