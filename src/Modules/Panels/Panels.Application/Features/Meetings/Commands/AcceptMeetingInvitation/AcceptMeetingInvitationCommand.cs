using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;

namespace Panels.Application.Features.Meetings.Commands.AcceptMeetingInvitation;

public class AcceptMeetingInvitationCommand : ICommand<Response>
{
    public int MeetingId { get; }
    public string InvitationCode { get; }
}