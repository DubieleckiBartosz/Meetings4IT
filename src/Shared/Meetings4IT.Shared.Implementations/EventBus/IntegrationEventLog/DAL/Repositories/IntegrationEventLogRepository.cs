using System.Data;
using Dapper;
using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess; 

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL.Repositories;

public class IntegrationEventLogRepository : BaseRepository<IntegrationEventLogContext>, IIntegrationEventLogRepository
{
    public IntegrationEventLogRepository(IntegrationEventLogContext dapperContext) : base(dapperContext)
    {
    }

    public Task<List<IntegrationEventLog>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@transactionId", transactionId);

        var result = QueryAsync<IntegrationEventLog>("", parameters);
        throw new NotImplementedException();
    }

    public Task SaveEventLogAsync(IntegrationEvent integrationEventLog, IDbTransaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task MarkEventAsPublishedAsync(Guid eventId)
    {
        return UpdateStatusAsync(eventId, EventState.Published);
    }

    public Task MarkEventAsInProgressAsync(Guid eventId)
    {
        return UpdateStatusAsync(eventId, EventState.InProgress);
    }

    public Task MarkEventAsFailedAsync(Guid eventId)
    {
        return UpdateStatusAsync(eventId, EventState.PublishedFailed);
    }
    private Task UpdateStatusAsync(Guid eventId, EventState status)
    {
        throw new NotImplementedException();
    }

}