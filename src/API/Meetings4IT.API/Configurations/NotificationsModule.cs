using Notifications.Core.Configurations;

namespace Meetings4IT.API.Configurations;

public static class NotificationsModule
{
    public static WebApplicationBuilder RegisterNotificationsModule(this WebApplicationBuilder builder)
    {
        builder
            .RegisterNotificationsOptions()
            .RegisterDatabaseNotifications()
            .RegisterDepenedencyInjectionNotifications();

        return builder;
    }

    public static WebApplication ConfigureNotifications(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.GetSection("UseNotificationsMigration").Get<bool>())
        {
            app.NotificationsMigration();
        }

        return app;
    }
}