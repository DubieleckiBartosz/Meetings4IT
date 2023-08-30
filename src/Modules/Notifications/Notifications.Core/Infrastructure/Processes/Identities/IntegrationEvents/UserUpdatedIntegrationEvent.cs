using Meetings4IT.Shared.Implementations.EventBus.Attributes;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Notifications.Core.Constants;

namespace Notifications.Core.Infrastructure.Processes.Identities.IntegrationEvents;

[IntegrationEventDecorator(Navigators.UserUpdatedNavigator)]
public record UserUpdatedIntegrationEvent(string Email) : IntegrationEvent;