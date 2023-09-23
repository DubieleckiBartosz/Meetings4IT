using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;

namespace Notifications.Core.Handlers.Panels.Commands;

public class InvitationAcceptedCommand : ICommand<Unit>
{
    public string MeetingLink { get; }
    public string RecipientId { get; }
    public string AcceptedBy { get; }

    public InvitationAcceptedCommand(string meetingLink, string recipientId, string acceptedBy)
    {
        MeetingLink = meetingLink;
        RecipientId = recipientId;
        AcceptedBy = acceptedBy;
    }
}