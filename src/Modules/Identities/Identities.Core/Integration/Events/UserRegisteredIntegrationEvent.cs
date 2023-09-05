using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Identities.Core.Integration.Events;

public record UserRegisteredIntegrationEvent(string Email, string UserName, string VerificationUri) : IntegrationEvent;