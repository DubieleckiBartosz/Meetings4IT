using Meetings4IT.Shared.Implementations.EventBus.Attributes;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Notifications.Core.Constants;

namespace Notifications.Core.Infrastructure.Processes.Identities.IntegrationEvents;

[IntegrationEventDecorator(Navigators.UserForgotPasswordNavigator)]
public record UserForgotPasswordIntegrationEvent(string Email, string Link, string Token) : IntegrationEvent;