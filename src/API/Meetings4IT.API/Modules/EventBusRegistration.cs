using Notifications.Core.Configurations;

namespace Meetings4IT.API.Modules;

public static class EventBusRegistration
{
    public static WebApplication RegisterEvents(this WebApplication app)
    {
        app.RegisterNotificationEvents();

        return app;
    }

    private static WebApplication RegisterNotificationEvents(this WebApplication app)
    {
        app.Initialize();

        return app;
    }
}