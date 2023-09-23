using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Panels.Application.IntegrationEvents.Events;

public record InvitationCreatedIntegrationEvent(string Recipient, string? RecipientId, string MeetingOrganizer, string MeetingLink, string InvitationLink) : IntegrationEvent;