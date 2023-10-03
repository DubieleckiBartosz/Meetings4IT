using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;

namespace Meetings4IT.IntegrationTests.Modules.Mocks;

public class TestIntegrationEventLogRepository : IIntegrationEventLogRepository
{
    public async Task MarkEventAsFailedAsync(Guid eventId)
    {
        await Task.CompletedTask;
    }

    public async Task MarkEventAsInProgressAsync(Guid eventId)
    {
        await Task.CompletedTask;
    }

    public async Task MarkEventAsPublishedAsync(Guid eventId)
    {
        await Task.CompletedTask;
    }

    public async Task SaveEventLogAsync(IntegrationEventLog integrationEventLog)
    {
        await Task.CompletedTask;
    }
}