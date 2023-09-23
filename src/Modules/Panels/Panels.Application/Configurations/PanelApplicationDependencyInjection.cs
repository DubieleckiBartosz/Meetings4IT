using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Panels.Application.IntegrationEvents;
using Panels.Domain.DomainServices;
using Panels.Domain.DomainServices.DomainServiceInterfaces;

namespace Panels.Application.Configurations;

public static class PanelApplicationDependencyInjection
{
    public static WebApplicationBuilder RegisterPanelApplicationDependencyInjection(this WebApplicationBuilder builder)
    {
        //Domain services
        builder.Services
            .AddSingleton<IMeetingDomainService, MeetingDomainService>()
            .AddSingleton<IUserDomainService, UserDomainService>()
            .AddTransient<IPanelIntegrationEventService, PanelIntegrationEventService>();

        return builder;
    }
}