using Meetings4IT.Shared.Implementations.EventBus.Attributes;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Notifications.Core.Constants;

namespace Notifications.Core.Infrastructure.Processes.Panels.IntegrationEvents;

[IntegrationEventDecorator(Navigators.InvitationAcceptedNavigator)]
public record InvitationAcceptedIntegrationEvent(string MeetingLink, string RecipientId, string AcceptedBy) : IntegrationEvent;