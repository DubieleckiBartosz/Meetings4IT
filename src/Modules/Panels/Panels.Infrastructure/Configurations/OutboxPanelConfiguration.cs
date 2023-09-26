using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panels.Infrastructure.Outbox;

namespace Panels.Infrastructure.Configurations;

public static class OutboxPanelConfiguration
{
    public static WebApplicationBuilder RegisterProcesses(this WebApplicationBuilder builder)
    {
        var outboxEnabled = builder.Configuration.GetSection("OutboxPanelOptions:Enabled").Get<bool>();
        if (outboxEnabled)
        {
            builder.Services.AddScoped<IOutboxPanelAccessor, OutboxPanelAccessor>();
            builder.Services.AddHostedService<OutboxPanelProcessor>();
        }

        return builder;
    }
}