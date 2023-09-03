using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Identities.Core.Integration.Events;

public record UserRegisteredIntegrationEvent(string Email, string VerificationUri) : IntegrationEvent;