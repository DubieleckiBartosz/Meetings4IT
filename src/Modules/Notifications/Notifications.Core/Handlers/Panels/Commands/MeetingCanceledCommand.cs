using MediatR;
using Meetings4IT.Shared.Implementations.Mediator;

namespace Notifications.Core.Handlers.Panels.Commands;

public record RecipientCanceledInvitationCommand(string Email, string? Identifier = null);

public class MeetingCanceledCommand : ICommand<Unit>
{
    public string MeetingLink { get; }
    public string MeetingOrganizer { get; }
    public List<RecipientCanceledInvitationCommand> Recipients { get; }

    public MeetingCanceledCommand(
        string meetingLink,
        string meetingOrganizer,
        List<RecipientCanceledInvitationCommand> recipients)
    {
        MeetingLink = meetingLink;
        MeetingOrganizer = meetingOrganizer;
        Recipients = recipients;
    }
}