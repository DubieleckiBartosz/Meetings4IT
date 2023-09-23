using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;

namespace Notifications.Core.Handlers.Panels.Commands;

public class InvitationRejectedCommand : ICommand<Unit>
{
    public string MeetingLink { get; }
    public string RecipientId { get; }
    public string RejectedBy { get; }

    public InvitationRejectedCommand(
        string meetingLink,
        string recipientId,
        string rejectedBy)
    {
        MeetingLink = meetingLink;
        RecipientId = recipientId;
        RejectedBy = rejectedBy;
    }
}