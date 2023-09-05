using Meetings4IT.Shared.Implementations.EventBus.InMemoryMessaging;
using Meetings4IT.Shared.Implementations.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Core.Reference;

namespace Notifications.Core.Configurations;

public static class EventInitiator
{
    public static void Initialize(this WebApplication app)
    {
        RegisterEvents(app);
    }
    private static WebApplication RegisterEvents(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IEventRegistry>();
        typeof(NotificationAssemblyReference).Assembly.RegistrationAssemblyIntegrationEvents(service);

        return app;
    }

}