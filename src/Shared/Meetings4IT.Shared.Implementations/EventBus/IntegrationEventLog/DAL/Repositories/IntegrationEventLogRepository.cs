using Dapper;
using Meetings4IT.Shared.Implementations.Dapper;
using System.Data;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;

public class IntegrationEventLogRepository : BaseRepository<IntegrationEventLogContext>, IIntegrationEventLogRepository
{
    public IntegrationEventLogRepository(IntegrationEventLogContext dapperContext) : base(dapperContext)
    {
    }

    public async Task SaveEventLogAsync(IntegrationEventLog integrationEventLog)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@eventId", integrationEventLog.EventId);
        parameters.Add("@creationTime", integrationEventLog.CreationTime);
        parameters.Add("@eventTypeName", integrationEventLog.EventTypeName);
        parameters.Add("@content", integrationEventLog.Content);
        parameters.Add("@state", integrationEventLog.State);
        parameters.Add("@timesSent", integrationEventLog.TimesSent);
        parameters.Add("@eventTypeShortName", integrationEventLog.EventTypeShortName);

        await ExecuteAsync("[logs].[integration_saveEventLog_I]", param: parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task MarkEventAsPublishedAsync(Guid eventId)
    {
        await UpdateStatusAsync(eventId, EventState.Published);
    }

    public async Task MarkEventAsInProgressAsync(Guid eventId)
    {
        await UpdateStatusAsync(eventId, EventState.InProgress, true);
    }

    public async Task MarkEventAsFailedAsync(Guid eventId)
    {
        await UpdateStatusAsync(eventId, EventState.PublishedFailed);
    }

    private async Task UpdateStatusAsync(Guid eventId, EventState status, bool sent = false)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@eventId", eventId);
        parameters.Add("@newStatus", status);
        parameters.Add("@sent", sent);

        await ExecuteAsync("[logs].[integration_updateEventLog_U]", param: parameters, commandType: CommandType.StoredProcedure);
    }
}