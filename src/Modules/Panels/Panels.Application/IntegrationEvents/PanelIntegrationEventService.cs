using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Meetings4IT.Shared.Implementations.Modules;

namespace Panels.Application.IntegrationEvents;

public class PanelIntegrationEventService : IPanelIntegrationEventService
{
    private readonly IIntegrationService _integrationService;

    public PanelIntegrationEventService(IIntegrationService integrationService)
    {
        _integrationService = integrationService;
    }

    public async Task PublishThroughEventBusAsync(IntegrationEvent evt, CancellationToken cancellationToken = default)
    {
        await _integrationService.PublishThroughEventBusAsync(evt, cancellationToken);
    }

    public async Task SaveEventAndPublishAsync(IntegrationEvent evt, CancellationToken cancellationToken = default)
    {
        var module = typeof(PanelAssemblyReference).ReadModuleName();
        await _integrationService.SaveEventAndPublishAsync(evt, module, cancellationToken);
    }
}