using Meetings4IT.Shared.Implementations.EventBus.InMemoryMessaging;
using Meetings4IT.Shared.Implementations.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Panels.Application.Configurations;

public static class EventPanelInitiator
{
    public static WebApplication InitializePanelEvents(this WebApplication app)
    {
        RegisterEvents(app);
        return app;
    }

    private static WebApplication RegisterEvents(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IEventRegistry>();
        typeof(PanelAssemblyReference).Assembly.RegistrationAssemblyIntegrationEvents(service);

        return app;
    }
}