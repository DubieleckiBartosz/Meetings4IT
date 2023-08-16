using Meetings4IT.Shared.Implementations.EventBus;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.Services;

namespace Panels.Application.IntegrationEvents;

public class PanelIntegrationEventService : IPanelIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly IIntegrationEventLogService _integrationEventLogService;

    public PanelIntegrationEventService(IEventBus eventBus, IIntegrationEventLogService integrationEventLogService)
    {
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _integrationEventLogService = integrationEventLogService ?? throw new ArgumentNullException(nameof(integrationEventLogService));
    }
}