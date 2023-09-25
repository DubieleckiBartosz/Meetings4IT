using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.Outbox;

//Inheritance of this interface in a specific module. Messages per schema
public interface IOutbox
{
    Task AddAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IntegrationEvent;

    Task SaveAsync();

    Task PublishUnsentAsync(CancellationToken cancellationToken = default);
}