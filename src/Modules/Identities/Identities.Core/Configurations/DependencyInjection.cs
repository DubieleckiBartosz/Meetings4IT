﻿using Identities.Core.DAL.Repositories;
using Identities.Core.DAL.Seed;
using Identities.Core.Integration;
using Identities.Core.Interfaces.Repositories;
using Identities.Core.Interfaces.Services;
using Identities.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Identities.Core.Configurations;

public static class DependencyInjection
{
    public static WebApplicationBuilder RegisterIdentityDependencyInjections(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<OpenIdAppConfigSeeder>();
        services.AddScoped<DataSeeder>();

        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddTransient<IIdentityIntegrationEventService, IdentityIntegrationEventService>();

        return builder;
    }
}