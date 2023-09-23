using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Panels.Application.IntegrationEvents.Events;

public record InvitationAcceptedIntegrationEvent(string MeetingLink, string RecipientId, string AcceptedBy) : IntegrationEvent;