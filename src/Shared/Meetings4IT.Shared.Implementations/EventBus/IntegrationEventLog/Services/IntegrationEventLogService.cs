using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.Services;

public class IntegrationEventLogService : IIntegrationEventLogService  
{
    private readonly IIntegrationEventLogRepository _integrationEventLogRepository; 

    public IntegrationEventLogService(IIntegrationEventLogRepository integrationEventLogRepository)
    {
        _integrationEventLogRepository = integrationEventLogRepository ?? throw new ArgumentNullException(nameof(integrationEventLogRepository)); 
    } 

    public async Task SaveEventAsync(IntegrationEvent @event)
    {
        var eventLog = new IntegrationEventLog(@event);

        await _integrationEventLogRepository.SaveEventLogAsync(eventLog);
    }

    public async Task MarkEventAsPublishedAsync(Guid eventId)
    {
        await _integrationEventLogRepository.MarkEventAsPublishedAsync(eventId);
    }

    public async Task MarkEventAsInProgressAsync(Guid eventId)
    {
        await _integrationEventLogRepository.MarkEventAsInProgressAsync(eventId);
    }

    public async Task MarkEventAsFailedAsync(Guid eventId)
    {
        await _integrationEventLogRepository.MarkEventAsFailedAsync(eventId);
    }
}