using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using System.Data;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.Services;

public interface IIntegrationEventLogService
{
    Task<List<IntegrationEventLog>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
    Task SaveEventAsync(IntegrationEvent @event, IDbTransaction transaction);
    Task MarkEventAsPublishedAsync(Guid eventId);
    Task MarkEventAsInProgressAsync(Guid eventId);
    Task MarkEventAsFailedAsync(Guid eventId);
}