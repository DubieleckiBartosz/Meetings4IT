using Notifications.Core.Configurations;
using Panels.Application.Configurations;

namespace Meetings4IT.API.Modules;

public static class EventBusRegistration
{
    public static WebApplication RegisterEvents(this WebApplication app)
    {
        app.InitializeNotificationEvents()
            .InitializePanelEvents();

        return app;
    }
}