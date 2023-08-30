using Identities.Core.Integration.Events;
using Identities.Core.Models.Entities.DomainEvents;
using Meetings4IT.Shared.Implementations.Decorators;

namespace Identities.Core.Integration.Handlers;

internal class UserRegisteredHandler : IDomainEventHandler<UserRegistered>
{
    private readonly IIdentityIntegrationEventService _integrationEventService;

    internal UserRegisteredHandler(IIdentityIntegrationEventService integrationEventService)
    {
        _integrationEventService = integrationEventService;
    }

    public async Task Handle(DomainEvent<UserRegistered> notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var @event = notification.Event;
        var result = new UserRegisteredIntegrationEvent(@event.Email, @event.TokenConfirmation);

        await _integrationEventService.SaveEventAndPublishAsync(result, cancellationToken);
    }
}