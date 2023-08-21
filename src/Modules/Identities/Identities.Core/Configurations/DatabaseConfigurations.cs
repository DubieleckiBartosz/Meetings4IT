using Identities.Core.DAL.Seed;
using Identities.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identities.Core.Configurations;

public static class DatabaseConfigurations
{
    public static void InitData(this IHost app, IConfiguration configuration)
    { 
        var dataInitOptions = configuration.GetSection("DataInitializationOptions").Get<DataInitializationOptions>()!;

        if (!dataInitOptions.InsertOpenIdDictApplicationConfigurations && !dataInitOptions.InsertUserData)
        {
            return;
        }

        var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory?.CreateScope();

        if (dataInitOptions.InsertOpenIdDictApplicationConfigurations)
        {
            var configSeeder = scope?.ServiceProvider.GetService<OpenIdAppConfigSeeder>();

            configSeeder?.SeedApplicationConfiguration()?.GetAwaiter().GetResult();
        }

        if (!dataInitOptions.InsertUserData && !dataInitOptions.InsertRoles)
        {
            return;
        }

        var dataSeeder = scope?.ServiceProvider.GetService<DataSeeder>();

        if (dataInitOptions.InsertRoles)
        {
            dataSeeder?.SeedRolesAsync()?.GetAwaiter().GetResult();
        }

        if (dataInitOptions.InsertUserData)
        {
            dataSeeder?.SeedAdminAsync()?.GetAwaiter().GetResult();
        }
    }
}