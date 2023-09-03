using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Tools;
using Notifications.Core.Handlers.Identities.Commands;
using Notifications.Core.Infrastructure.Processes.Identities.IntegrationEvents;
using Serilog;

namespace Notifications.Core.Infrastructure.Processes.Identities;

public class ProcessUserRegistered : IEventHandler<UserRegisteredIntegrationEvent>
{
    private readonly ICommandBus _commandBus;
    private readonly ILogger _logger;

    public ProcessUserRegistered(ICommandBus commandBus, ILogger logger)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(UserRegisteredIntegrationEvent notification, CancellationToken cancellationToken)
    {
        _logger.Information($"Event processing {notification.GetType().Name}: {notification.Serialize()}");

        await _commandBus.Send(new UserRegisteredCommand(notification.Email, notification.VerificationUri));
    }
}