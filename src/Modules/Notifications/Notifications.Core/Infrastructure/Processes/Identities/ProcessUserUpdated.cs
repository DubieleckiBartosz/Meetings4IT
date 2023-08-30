using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.Mediator;
using Notifications.Core.Infrastructure.Processes.Identities.IntegrationEvents;

namespace Notifications.Core.Infrastructure.Processes.Identities;

public class ProcessUserUpdated : IEventHandler<UserUpdatedIntegrationEvent>
{
    private readonly ICommandBus _commandBus;

    public ProcessUserUpdated(ICommandBus commandBus)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
    }

    public Task Handle(UserUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}