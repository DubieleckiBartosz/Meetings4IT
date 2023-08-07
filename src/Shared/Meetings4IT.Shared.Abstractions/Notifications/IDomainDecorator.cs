namespace Meetings4IT.Shared.Abstractions.Notifications;

public interface IDomainDecorator
{
    Task Publish<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification : IDomainNotification;
}