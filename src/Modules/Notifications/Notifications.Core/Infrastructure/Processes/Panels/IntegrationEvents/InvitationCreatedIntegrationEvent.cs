using Meetings4IT.Shared.Implementations.EventBus.Attributes;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Notifications.Core.Constants;

namespace Notifications.Core.Infrastructure.Processes.Panels.IntegrationEvents;

[IntegrationEventDecorator(Navigators.InvitationCreatedNavigator)]
public record InvitationCreatedIntegrationEvent(string Recipient, string? RecipientId, string MeetingOrganizer, string Meeting, string Code) : IntegrationEvent;