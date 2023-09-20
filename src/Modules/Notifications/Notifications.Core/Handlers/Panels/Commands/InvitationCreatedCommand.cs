using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;

namespace Notifications.Core.Handlers.Panels.Commands;

public class InvitationCreatedCommand : ICommand<Unit>
{
    public string Recipient { get; }
    public string? RecipientId { get; }
    public string MeetingOrganizer { get; }
    public string Meeting { get; }
    public string Code { get; }

    public InvitationCreatedCommand(
        string recipient,
        string? recipientId,
        string meetingOrganizer,
        string meeting,
        string code)
    {
        Recipient = recipient;
        RecipientId = recipientId;
        MeetingOrganizer = meetingOrganizer;
        Meeting = meeting;
        Code = code;
    }
}