using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Panels.Infrastructure.Outbox;

namespace Panels.Infrastructure.Configurations;

public static class OutboxPanelConfiguration
{
    public static WebApplicationBuilder RegisterProcesses(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<OutboxPanelProcessor>();
        return builder;
    }
}