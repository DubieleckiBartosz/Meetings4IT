using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using System.Data;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.Services;

public class IntegrationEventLogService : IIntegrationEventLogService  
{
    private readonly IIntegrationEventLogRepository _integrationEventLogRepository;
    private readonly List<Type?> _eventTypes;

    public IntegrationEventLogService(IIntegrationEventLogRepository integrationEventLogRepository, List<Type?> eventTypes)
    {
        _integrationEventLogRepository = integrationEventLogRepository;
        _eventTypes = eventTypes ?? throw new ArgumentNullException(nameof(eventTypes));
    }
    public async Task<List<IntegrationEventLog>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
    {
        var result = await _integrationEventLogRepository.RetrieveEventLogsPendingToPublishAsync(transactionId);

        if (result.Any())
        {
            return result.OrderBy(o => o.CreationTime)
                .Select(e => e.DeserializeJsonContent(_eventTypes.Find(_ => _.Name == e.EventTypeShortName)!)).ToList();
        }

        return new List<IntegrationEventLog>();
    }

    public async Task SaveEventAsync(IntegrationEvent @event, IDbTransaction transaction)
    {
       await _integrationEventLogRepository.SaveEventLogAsync(@event, transaction);
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