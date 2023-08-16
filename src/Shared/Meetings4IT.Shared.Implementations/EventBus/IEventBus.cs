using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus;

public interface IEventBus
{
    Task Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent;
    void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IntegrationEvent;
}