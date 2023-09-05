using Identities.Core.Reference;
using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.Services;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Meetings4IT.Shared.Implementations.Modules;
using Serilog;

namespace Identities.Core.Integration;

internal class IdentityIntegrationEventService : IIdentityIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly IIntegrationEventLogService _integrationEventLogService;
    private readonly ILogger _logger;

    public IdentityIntegrationEventService(IEventBus eventBus, IIntegrationEventLogService integrationEventLogService,
        ILogger logger)
    {
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _integrationEventLogService = integrationEventLogService ??
                                      throw new ArgumentNullException(nameof(integrationEventLogService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task PublishThroughEventBusAsync(IntegrationEvent evt, CancellationToken cancellationToken = default)
    {
        await _eventBus.PublishAsync(cancellationToken, evt);
    }

    public async Task SaveEventAndPublishAsync(IntegrationEvent evt, CancellationToken cancellationToken = default)
    {
        _logger.Information(
            "----- IdentityIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

        await _integrationEventLogService.SaveEventAsync(evt);

        await MarkAndPublishAsync(evt, cancellationToken);
    }

    private async Task MarkAndPublishAsync(IntegrationEvent evt, CancellationToken cancellationToken = default)
    {
        var module = typeof(IdentityAssemblyReference).ReadModuleName();

        try
        {
            _logger.Information(
                "----- Publishing integration event: {IntegrationEventId_published} from {AppName} - ({@IntegrationEvent})",
                evt.Id, module, evt);

            await _integrationEventLogService.MarkEventAsInProgressAsync(evt.Id);
            await PublishThroughEventBusAsync(evt, cancellationToken);
            await _integrationEventLogService.MarkEventAsPublishedAsync(evt.Id);
        }
        catch (Exception ex)
        {
            _logger.Error(ex,
                "ERROR Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})",
                evt.Id, module, evt);
            await _integrationEventLogService.MarkEventAsFailedAsync(evt.Id);
        }
    }
}