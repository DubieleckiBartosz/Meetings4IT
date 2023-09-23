using Identities.Core.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Identities.Core.Configurations;

public static class OptionsConfigurations
{
    public static WebApplicationBuilder RegisterIdentitiesOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<IdentityPathOptions>(builder.Configuration.GetSection("IdentityPathOptions"));

        return builder;
    }
}