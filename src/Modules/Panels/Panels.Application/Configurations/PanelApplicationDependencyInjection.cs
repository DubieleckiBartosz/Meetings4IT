using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Panels.Domain.DomainServices;

namespace Panels.Application.Configurations;

public static class PanelApplicationDependencyInjection
{
    public static WebApplicationBuilder RegisterPanelApplicationDependencyInjection(this WebApplicationBuilder builder)
    {
        //Domain services
        builder.Services.AddSingleton<IMeetingDomainService, MeetingDomainService>();

        return builder;
    }
}