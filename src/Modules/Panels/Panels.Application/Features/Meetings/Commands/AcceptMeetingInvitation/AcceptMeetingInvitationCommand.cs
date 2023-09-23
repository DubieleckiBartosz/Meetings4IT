using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Panels.Application.Models.Parameters;

namespace Panels.Application.Features.Meetings.Commands.AcceptMeetingInvitation;

public class AcceptMeetingInvitationCommand : ICommand<Response>
{
    public int MeetingId { get; }
    public string InvitationCode { get; }

    public AcceptMeetingInvitationCommand(AcceptMeetingInvitationParameters parameters)
    {
        MeetingId = parameters.MeetingId;
        InvitationCode = parameters.InvitationCode;
    }
}