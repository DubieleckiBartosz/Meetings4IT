using Meetings4IT.Shared.Implementations.EventBus.Attributes;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Notifications.Core.Constants;

namespace Notifications.Core.Infrastructure.Processes.Panels.IntegrationEvents;

[IntegrationEventDecorator(Navigators.InvitationRejectedNavigator)]
public record InvitationRejectedIntegrationEvent(string MeetingLink, string RecipientId, string RejectedBy) : IntegrationEvent;