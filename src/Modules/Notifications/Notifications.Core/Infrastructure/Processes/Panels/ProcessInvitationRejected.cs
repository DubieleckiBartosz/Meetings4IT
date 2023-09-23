using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Tools;
using Notifications.Core.Handlers.Panels.Commands;
using Notifications.Core.Infrastructure.Processes.Panels.IntegrationEvents;
using Serilog;

namespace Notifications.Core.Infrastructure.Processes.Panels;

public class ProcessInvitationRejected : IEventHandler<InvitationRejectedIntegrationEvent>
{
    private readonly ICommandBus _commandBus;
    private readonly ILogger _logger;

    public ProcessInvitationRejected(ICommandBus commandBus, ILogger logger)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(InvitationRejectedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        _logger.Information($"Event processing {notification.GetType().Name}: {notification.Serialize()}");

        await _commandBus.Send(new InvitationRejectedCommand(notification.MeetingLink, notification.RecipientId, notification.RejectedBy), cancellationToken);
    }
}