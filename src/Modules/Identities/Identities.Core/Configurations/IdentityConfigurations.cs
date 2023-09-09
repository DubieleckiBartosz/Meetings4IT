using Identities.Core.DAL;
using Identities.Core.Models.Entities;
using Identities.Core.Models.Entities.OpenIdDictCustomEntities;
using Meetings4IT.Shared.Implementations;
using Meetings4IT.Shared.Implementations.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identities.Core.Configurations;

public static class IdentityConfigurations
{
    //https://learn.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-7.0
    public static WebApplicationBuilder RegisterIdentity(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;

        builder.RegisterEntityFrameworkSqlServer<ApplicationDbContext>(options, _ =>
            _.UseOpenIddict<CustomApplicationEntity, CustomAuthorizationEntity, CustomScopeEntity, CustomTokenEntity, string>());

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return builder;
    }
}