using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Panels.Application.IntegrationEvents;

public interface IPanelIntegrationEventService
{
    Task PublishThroughEventBusAsync(IntegrationEvent evt, CancellationToken cancellationToken = default);

    Task SaveEventAndPublishAsync(IntegrationEvent evt, CancellationToken cancellationToken = default);
}