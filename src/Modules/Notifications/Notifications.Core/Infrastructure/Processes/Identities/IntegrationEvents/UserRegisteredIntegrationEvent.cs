using Meetings4IT.Shared.Implementations.EventBus.Attributes;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Notifications.Core.Constants;

namespace Notifications.Core.Infrastructure.Processes.Identities.IntegrationEvents;

[IntegrationEventDecorator(Navigators.UserRegisteredNavigator)]
public record UserRegisteredIntegrationEvent(string Email, string UserName, string VerificationUri) : IntegrationEvent;