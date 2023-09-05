using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus;

public interface IEventBus
{
    Task PublishAsync<TEvent>(CancellationToken cancellationToken, params TEvent[] @events) where TEvent : IntegrationEvent;
}