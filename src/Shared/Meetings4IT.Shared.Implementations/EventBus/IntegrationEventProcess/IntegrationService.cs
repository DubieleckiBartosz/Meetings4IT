using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.Services;
using Serilog;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

public class IntegrationService : IIntegrationService
{
    private readonly ILogger _logger;
    private readonly IEventBus _eventBus;
    private readonly IIntegrationEventLogService _integrationEventLogService;

    public IntegrationService(ILogger logger, IEventBus eventBus, IIntegrationEventLogService integrationEventLogService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _integrationEventLogService = integrationEventLogService ?? throw new ArgumentNullException(nameof(integrationEventLogService));
    }

    public async Task PublishThroughEventBusAsync(IntegrationEvent evt, CancellationToken cancellationToken = default)
    {
        await _eventBus.PublishAsync(cancellationToken, evt);
    }

    public async Task SaveEventAndPublishAsync(IntegrationEvent evt, string app, CancellationToken cancellationToken = default)
    {
        _logger.Information($"----- IntegrationEventService - Saving changes and integrationEvent: {evt.Id}");

        await _integrationEventLogService.SaveEventAsync(evt);

        await MarkAndPublishAsync(evt, app, cancellationToken);
    }

    private async Task MarkAndPublishAsync(IntegrationEvent evt, string app, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.Information($"----- Publishing integration event: {evt.Id} from {app} - ({evt})");

            await _integrationEventLogService.MarkEventAsInProgressAsync(evt.Id);
            await PublishThroughEventBusAsync(evt, cancellationToken);
            await _integrationEventLogService.MarkEventAsPublishedAsync(evt.Id);
        }
        catch (Exception ex)
        {
            _logger.Error($"ERROR Publishing integration event: {evt.Id} from {app} - ({evt}) {ex}");
            await _integrationEventLogService.MarkEventAsFailedAsync(evt.Id);
        }
    }
}