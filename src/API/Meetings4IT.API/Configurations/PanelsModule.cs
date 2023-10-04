using Panels.Application.Configurations;
using Panels.Infrastructure.Configurations;

namespace Meetings4IT.API.Configurations;

public static class PanelsModule
{
    public static WebApplicationBuilder RegisterPanelsModule(this WebApplicationBuilder builder)
    {
        builder
            .PanelsQuartzConfiguration()
            .RegisterPanelsOptions()
            .RegisterDatabasePanels()
            .RegisterProcesses()
            .RegisterPanelApplicationDependencyInjection()
            .RegisterPanelInfrastructureDependencyInjection();

        return builder;
    }

    public static WebApplication ConfigurePanels(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.GetSection("UsePanelsMigration").Get<bool>())
        {
            app.PanelsMigration();
        }

        return app;
    }
}