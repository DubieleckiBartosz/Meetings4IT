using Meetings4IT.Shared.Implementations.EventBus.Dispatchers;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus.Messaging;

public class InMemoryEventBus : IEventBus
{
    private readonly IAsyncEventDispatcher _dispatcher;

    public InMemoryEventBus(IAsyncEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
    }

    public async Task PublishAsync<TEvent>(CancellationToken cancellationToken, params TEvent[] @events) where TEvent : IntegrationEvent
    {
        var tasks = @events.Select(@event =>
              _dispatcher.PublishAsync(@event, cancellationToken));

        await Task.WhenAll(tasks);
    }
}