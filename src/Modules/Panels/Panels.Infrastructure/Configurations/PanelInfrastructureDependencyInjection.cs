using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Panels.Application.Contracts.Repositories;
using Panels.Infrastructure.Outbox;
using Panels.Infrastructure.Repositories;

namespace Panels.Infrastructure.Configurations;

public static class PanelInfrastructureDependencyInjection
{
    public static WebApplicationBuilder RegisterPanelInfrastructureDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IOutboxPanelAccessor, OutboxPanelAccessor>()
            .AddScoped<IMeetingRepository, MeetingRepository>()
            .AddScoped<IScheduledMeetingRepository, ScheduledMeetingRepository>()
            .AddScoped<IMeetingCategoryRepository, MeetingCategoryRepository>()
            .AddScoped<ITechnologyRepository, TechnologyRepository>()
            .AddScoped<IUserRepository, UserRepository>();

        return builder;
    }
}