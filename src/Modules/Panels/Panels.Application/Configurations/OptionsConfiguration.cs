using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panels.Application.Options;

namespace Panels.Application.Configurations;

public static class OptionsConfiguration
{
    public static WebApplicationBuilder RegisterPanelsOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<PanelPathOptions>(builder.Configuration.GetSection("PanelPathOptions"));
        builder.Services.Configure<OutboxPanelOptions>(builder.Configuration.GetSection("OutboxPanelOptions"));
        return builder;
    }
}