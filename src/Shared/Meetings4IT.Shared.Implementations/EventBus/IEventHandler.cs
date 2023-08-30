using MediatR;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IntegrationEvent
{
}