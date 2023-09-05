namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;

public interface IIntegrationEventLogRepository
{
    Task SaveEventLogAsync(IntegrationEventLog integrationEventLog);
    Task MarkEventAsPublishedAsync(Guid eventId);
    Task MarkEventAsInProgressAsync(Guid eventId);
    Task MarkEventAsFailedAsync(Guid eventId);
}