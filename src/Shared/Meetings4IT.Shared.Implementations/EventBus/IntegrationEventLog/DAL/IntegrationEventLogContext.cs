using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL;

public class IntegrationEventLogContext : DapperContext
{
    public IntegrationEventLogContext(IOptions<LogOptions> dbConnectionOptions, ILogger logger)
        : base(dbConnectionOptions!.Value.LogConnection, logger)
    {
    }
}