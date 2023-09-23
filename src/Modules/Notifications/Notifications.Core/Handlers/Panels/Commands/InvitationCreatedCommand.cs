using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;

namespace Notifications.Core.Handlers.Panels.Commands;

public class InvitationCreatedCommand : ICommand<Unit>
{
    public string Recipient { get; }
    public string? RecipientId { get; }
    public string MeetingOrganizer { get; }
    public string MeetingLink { get; }
    public string InvitationLink { get; }

    public InvitationCreatedCommand(
        string recipient,
        string? recipientId,
        string meetingOrganizer,
        string meetingLink,
        string invitationLink)
    {
        Recipient = recipient;
        RecipientId = recipientId;
        MeetingOrganizer = meetingOrganizer;
        MeetingLink = meetingLink;
        InvitationLink = invitationLink;
    }
}