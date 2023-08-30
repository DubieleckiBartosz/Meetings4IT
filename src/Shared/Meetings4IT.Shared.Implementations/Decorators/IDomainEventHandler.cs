using MediatR;
using Meetings4IT.Shared.Abstractions.Events;

namespace Meetings4IT.Shared.Implementations.Decorators;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<DomainEvent<TDomainEvent>> where TDomainEvent : IDomainEvent 
{
}