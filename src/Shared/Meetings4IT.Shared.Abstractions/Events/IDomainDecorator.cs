namespace Meetings4IT.Shared.Abstractions.Events;

public interface IDomainDecorator
{
    Task Publish<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification : IDomainEvent;
}