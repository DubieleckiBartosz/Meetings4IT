using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus.Dispatchers;

public interface IAsyncEventDispatcher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IntegrationEvent;
}