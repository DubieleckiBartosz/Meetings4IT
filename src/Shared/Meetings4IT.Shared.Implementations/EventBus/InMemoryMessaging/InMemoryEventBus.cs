using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus.InMemoryMessaging;

public class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, IIntegrationEventHandler> _handlers = new(); 

    public async Task Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent
    {
        if (_handlers[@event.GetType()] is IIntegrationEventHandler<TEvent> handler)
        {
            await handler.Handle(@event);
            return;
        }

        throw new NotImplementedException($"The {@event.GetType()} - {@event.Id} event handler has not been registered.");
    }

    public void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IntegrationEvent
    {
        _handlers.Add(typeof(TEvent), handler);
    }
}