using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Tools;
using Notifications.Core.Handlers.Panels.Commands;
using Notifications.Core.Infrastructure.Processes.Panels.IntegrationEvents;
using Serilog;

namespace Notifications.Core.Infrastructure.Processes.Panels;

public class ProcessMeetingCanceled : IEventHandler<MeetingCanceledIntegrationEvent>
{
    private readonly ICommandBus _commandBus;
    private readonly ILogger _logger;

    public ProcessMeetingCanceled(ICommandBus commandBus, ILogger logger)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(MeetingCanceledIntegrationEvent notification, CancellationToken cancellationToken)
    {
        _logger.Information($"Event processing {notification.GetType().Name}: {notification.Serialize()}");

        var recipients = notification.Recipients.Select(_ => new RecipientCanceledInvitationCommand(_.Email, _.Identifier)).ToList();
        await _commandBus.Send((new MeetingCanceledCommand(notification.MeetingLink, notification.MeetingOrganizer, recipients)), cancellationToken);
    }
}