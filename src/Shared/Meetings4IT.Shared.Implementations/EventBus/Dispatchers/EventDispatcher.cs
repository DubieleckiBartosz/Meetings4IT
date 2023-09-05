using MediatR;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus.Dispatchers;

public class EventDispatcher : IEventDispatcher
{
    private readonly IMediator _mediator;

    public EventDispatcher(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task PublishAsync(params IntegrationEvent[] @events)
    {
        foreach (var @event in @events)
        {
            await _mediator.Publish(@event);
        }
    }
}