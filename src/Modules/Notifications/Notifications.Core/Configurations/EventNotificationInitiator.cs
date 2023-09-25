using Meetings4IT.Shared.Implementations.EventBus.Messaging;
using Meetings4IT.Shared.Implementations.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Core.Reference;

namespace Notifications.Core.Configurations;

public static class EventNotificationInitiator
{
    public static WebApplication InitializeNotificationEvents(this WebApplication app)
    {
        RegisterEvents(app);
        return app;
    }

    private static WebApplication RegisterEvents(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IEventRegistry>();
        typeof(NotificationAssemblyReference).Assembly.RegistrationAssemblyIntegrationEvents(service);

        return app;
    }
}