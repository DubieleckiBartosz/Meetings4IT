using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Tools;
using Panels.Application.Features.Users.Commands.CreateNewUser;
using Panels.Application.IntegrationEvents.Processing.IdentityProcessingEvents;
using Serilog;

namespace Panels.Application.IntegrationEvents.Processing;

public class ProcessingUserConfirmed : IEventHandler<UserConfirmedIntegrationEvent>
{
    private readonly ICommandBus _commandBus;
    private readonly ILogger _logger;

    public ProcessingUserConfirmed(ICommandBus commandBus, ILogger logger)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(UserConfirmedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        _logger.Information($"Event processing {notification.GetType().Name}: {notification.Serialize()}");

        await _commandBus.Send(new CreateNewUserCommand(notification.Email, notification.Name, notification.UserId));
    }
}