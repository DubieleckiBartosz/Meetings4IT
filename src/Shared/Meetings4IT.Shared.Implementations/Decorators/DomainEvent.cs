using MediatR;
using Meetings4IT.Shared.Abstractions.Events;

namespace Meetings4IT.Shared.Implementations.Decorators;

public class DomainEvent<T> : INotification where T : IDomainEvent
{
    public T Event { get; }

    public DomainEvent(T @event)
    {
        this.Event = @event;
    }
}