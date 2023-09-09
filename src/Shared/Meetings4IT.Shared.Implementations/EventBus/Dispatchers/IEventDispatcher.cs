using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus.Dispatchers;

public interface IEventDispatcher
{
    Task PublishAsync(CancellationToken cancellationToken = default, params IntegrationEvent[] @events);
}