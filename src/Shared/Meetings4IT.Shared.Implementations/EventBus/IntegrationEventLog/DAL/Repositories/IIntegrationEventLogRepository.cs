using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using System.Data;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;

public interface IIntegrationEventLogRepository
{
    Task<List<IntegrationEventLog>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
    Task SaveEventLogAsync(IntegrationEvent integrationEventLog, IDbTransaction transaction);
    Task MarkEventAsPublishedAsync(Guid eventId);
    Task MarkEventAsInProgressAsync(Guid eventId);
    Task MarkEventAsFailedAsync(Guid eventId);
}