using Identities.Core.Configurations;
using Identities.Core.Reference;
using Meetings4IT.Shared.Implementations;

namespace Meetings4IT.API.Configurations;

public static class IdentitiesModule
{
    public static WebApplicationBuilder RegisterIdentitiesModule(this WebApplicationBuilder builder)
    {
        builder
            .RegisterIdentitiesOptions()
            .RegisterOpenIdDict()
            .RegisterIdentity()
            .RegisterIdentityDependencyInjections();

        builder.RegisterUserAccessor()
            .Services.RegisterAutoMapper(typeof(IdentityAssemblyReference));

        return builder;
    }

    public static WebApplication ConfigureIdentities(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.GetSection("UseIdentityMigration").Get<bool>())
        {
            app.IdentitiesMigration();
        }

        app.InitDataIdentitiesModule(configuration);

        return app;
    }

    private static void InitDataIdentitiesModule(this IHost host, IConfiguration configuration)
    {
        host.InitData(configuration);
    }
}