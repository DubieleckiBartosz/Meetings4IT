using Panels.Application.Configurations;
using Panels.Infrastructure.Configurations;

namespace Meetings4IT.API.Configurations;

public static class PanelsModule
{
    public static WebApplicationBuilder RegisterPanelsModule(this WebApplicationBuilder builder)
    {
        builder
            .RegisterDatabasePanels()
            .RegisterPanelApplicationDependencyInjection()
            .RegisterPanelInfrastructureDependencyInjection();

        return builder;
    }

    public static WebApplication ConfigurePanels(this WebApplication app)
    {
        app.PanelsMigration();

        return app;
    }
}