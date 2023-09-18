using Meetings4IT.Shared.Implementations;
using Meetings4IT.Shared.Implementations.EntityFramework;
using Meetings4IT.Shared.Implementations.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Panels.Infrastructure.Database;

namespace Panels.Infrastructure.Configurations;

public static class DatabaseConfigurations
{
    public static WebApplicationBuilder RegisterDatabasePanels(this WebApplicationBuilder builder)
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddConsole() // Log to the console
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
        });

        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;
        builder.RegisterEntityFrameworkSqlServer<PanelContext>(options, loggerFactory: loggerFactory);

        return builder;
    }

    public static WebApplication PanelsMigration(this WebApplication app)
    {
        app.RunMigration<PanelContext>();

        return app;
    }
}