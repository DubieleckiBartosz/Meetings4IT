namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

public interface IIntegrationService
{
    Task PublishThroughEventBusAsync(IntegrationEvent evt, CancellationToken cancellationToken = default);

    Task SaveEventAndPublishAsync(IntegrationEvent evt, string app, CancellationToken cancellationToken = default);
}