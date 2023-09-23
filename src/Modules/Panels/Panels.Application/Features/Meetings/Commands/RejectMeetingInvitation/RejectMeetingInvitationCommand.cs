using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters;

namespace Panels.Application.Features.Meetings.Commands.RejectMeetingInvitation;

public class RejectMeetingInvitationCommand : ICommand<Response>
{
    public int MeetingId { get; }
    public string InvitationCode { get; }

    public RejectMeetingInvitationCommand(RejectMeetingInvitationParameters parameters)
    {
        MeetingId = parameters.MeetingId;
        InvitationCode = parameters.InvitationCode;
    }
}