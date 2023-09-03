using Meetings4IT.Shared.Implementations;
using Meetings4IT.Shared.Implementations.EntityFramework;
using Meetings4IT.Shared.Implementations.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Notifications.Core.Infrastructure.Database;

namespace Notifications.Core.Configurations;

public static class DatabaseConfigurations
{
    public static WebApplicationBuilder RegisterDatabaseNotifications(this WebApplicationBuilder builder)
    {
        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;
        builder.RegisterEntityFrameworkSqlServer<NotificationContext>(options);

        return builder;
    }

    public static WebApplication NotificationsMigration(this WebApplication app)
    {
        app.RunMigration<NotificationContext>();

        return app;
    }
}