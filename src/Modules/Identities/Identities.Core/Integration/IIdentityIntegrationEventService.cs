using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Identities.Core.Integration;

internal interface IIdentityIntegrationEventService
{
    Task SaveEventAndPublishAsync(IntegrationEvent evt, CancellationToken cancellationToken = default);

    Task PublishThroughEventBusAsync(IntegrationEvent evt, CancellationToken cancellationToken = default);
}