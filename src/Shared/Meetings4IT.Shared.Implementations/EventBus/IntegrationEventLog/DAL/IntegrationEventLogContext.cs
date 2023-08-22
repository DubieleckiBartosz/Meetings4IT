﻿using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace Meetings4IT.Shared.Implementations.EventBus.IntegrationEventLog.DAL;

public class IntegrationEventLogContext : DapperContext
{
    public IntegrationEventLogContext(IOptions<DapperOptions> dbConnectionOptions, ILogger logger) : base(dbConnectionOptions, logger)
    {
    }
}