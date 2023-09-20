using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Tools;
using Notifications.Core.Handlers.Panels.Commands;
using Notifications.Core.Infrastructure.Processes.Panels.IntegrationEvents;
using Serilog;

namespace Notifications.Core.Infrastructure.Processes.Panels;

public class ProcessInvitationCreated : IEventHandler<InvitationCreatedIntegrationEvent>
{
    private readonly ICommandBus _commandBus;
    private readonly ILogger _logger;

    public ProcessInvitationCreated(ICommandBus commandBus, ILogger logger)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(InvitationCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        _logger.Information($"Event processing {notification.GetType().Name}: {notification.Serialize()}");

        await _commandBus.Send(new InvitationCreatedCommand(notification.Recipient, notification.RecipientId,
                          notification.MeetingOrganizer, notification.Meeting, notification.Code), cancellationToken);
    }
}