using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Panels.Application.IntegrationEvents.Events;

//When the identifier is null, there is no alert because the user doesn't belong to the system
public record RecipientCanceledInvitation(string Email, string? Identifier = null);

public record MeetingCanceledIntegrationEvent(string MeetingLink, string MeetingOrganizer, List<RecipientCanceledInvitation> Recipients) : IntegrationEvent
{
    public static MeetingCanceledIntegrationEvent Create(string meetingLink, string meetingOrganizer, List<RecipientCanceledInvitation> recipients)
    {
        return new MeetingCanceledIntegrationEvent(meetingLink, meetingOrganizer, recipients);
    }
}