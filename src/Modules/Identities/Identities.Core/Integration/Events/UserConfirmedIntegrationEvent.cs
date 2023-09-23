using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Identities.Core.Integration.Events;

public record UserConfirmedIntegrationEvent(string Email, string Name, string UserId) : IntegrationEvent;