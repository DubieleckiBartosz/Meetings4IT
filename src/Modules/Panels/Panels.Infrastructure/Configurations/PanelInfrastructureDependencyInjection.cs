﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Panels.Application.Contracts.ReadRepositories;
using Panels.Application.Contracts.Repositories;
using Panels.Infrastructure.Repositories;
using Panels.Infrastructure.Repositories.ReadRepositories;

namespace Panels.Infrastructure.Configurations;

public static class PanelInfrastructureDependencyInjection
{
    public static WebApplicationBuilder RegisterPanelInfrastructureDependencyInjection(this WebApplicationBuilder builder)
    {
        //IOutboxPanelAccessor is registered in the OutboxPanelConfiguration class
        builder.Services
            .AddScoped<IMeetingRepository, MeetingRepository>()
            .AddScoped<IScheduledMeetingRepository, ScheduledMeetingRepository>()
            .AddScoped<IMeetingCategoryRepository, MeetingCategoryRepository>()
            .AddScoped<ITechnologyRepository, TechnologyRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IMeetingReadRepository, MeetingReadRepository>()
            .AddScoped<IUserReadRepository, UserReadRepository>();

        return builder;
    }
}