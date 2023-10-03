using Identities.Core.DAL;
using Identities.Core.Managers;
using Identities.Core.Models.Entities.OpenIdDictCustomEntities;
using Identities.Core.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;

namespace Identities.Core.Configurations;

public static class OpenIdDictConfigurations
{
    public static WebApplicationBuilder RegisterOpenIdDict(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<OpenIdDictOptions>(builder.Configuration.GetSection("OpenIdDictOptions"));
        var openIdDictOptions = builder.Configuration.GetSection("OpenIdDictOptions").Get<OpenIdDictOptions>();

        builder.Services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>()
                    .ReplaceDefaultEntities<CustomApplicationEntity, CustomAuthorizationEntity, CustomScopeEntity,
                        CustomTokenEntity, string>();

                options.ReplaceAuthorizationManager<AuthorizationManager>();
            }).AddServer(options =>
            {
                options
                    .SetTokenEndpointUris("connect/token")
                    .SetRevocationEndpointUris("connect/revoke")
                    .SetIntrospectionEndpointUris("connect/introspect");

                options
                    .AllowPasswordFlow()
                    .AllowRefreshTokenFlow();

                options.UseReferenceRefreshTokens();
                options.RegisterScopes(
                    OpenIddictConstants.Scopes.OpenId,
                    OpenIddictConstants.Scopes.Profile,
                    //https://github.com/manfredsteyer/angular-oauth2-oidc/issues/1241
                    OpenIddictConstants.Scopes.OfflineAccess,
                    openIdDictOptions!.Scope);

                options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30))
                    .SetRefreshTokenLifetime(TimeSpan.FromDays(7));

                if (!builder.Environment.IsProduction())
                {
                    //Local tests

                    options
                    .AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                    options.DisableAccessTokenEncryption();
                }
                else
                {
                    //https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html

                    //registration real certification
                }

                options
                    .UseAspNetCore()
                    .EnableLogoutEndpointPassthrough()
                    .EnableTokenEndpointPassthrough();
            }).AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
                options.EnableAuthorizationEntryValidation();
                options.EnableTokenEntryValidation();
            })
            .Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        return builder;
    }
}