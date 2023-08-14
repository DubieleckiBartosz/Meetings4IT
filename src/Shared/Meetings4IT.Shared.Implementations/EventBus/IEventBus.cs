using Meetings4IT.Shared.Implementations.EventBus.IntegrationEvent;

namespace Meetings4IT.Shared.Implementations.EventBus;

internal interface IEventBus
{
    Task Publish<TEvent>(TEvent @event)
        where TEvent : IntegrationEvent.IntegrationEvent;

    void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler)
        where TEvent : IntegrationEvent.IntegrationEvent;
}