using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Identities.Core.Integration.Events;

public record UserForgotPasswordIntegrationEvent(string Email, string Link, string Token) : IntegrationEvent;