using Identities.Core.Configurations;
using Identities.Core.Reference;
using Meetings4IT.Shared.Implementations;

namespace Meetings4IT.API.Configurations;

public static class IdentityModule
{
    public static WebApplicationBuilder RegisterIdentityModule(this WebApplicationBuilder builder)
    {
        builder
            .RegisterOpenIdDict()
            .RegisterIdentity()
            .RegisterIdentityDependencyInjections();

        builder.RegisterUserAccessor()
            .Services.RegisterAutoMapper(typeof(IdentityAssemblyReference));

        return builder;
    }
    
    public static void InitDataIdentityModule(this IHost host, IConfiguration configuration)
    {
        host.InitData(configuration);
    }
}