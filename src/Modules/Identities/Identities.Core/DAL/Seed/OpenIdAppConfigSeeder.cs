using Identities.Core.Options;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;

namespace Identities.Core.DAL.Seed;

public class OpenIdAppConfigSeeder
{
    private readonly IOpenIddictApplicationManager _manager;
    private readonly OpenIdDictOptions _options;

    public OpenIdAppConfigSeeder(IOpenIddictApplicationManager manager, IOptions<OpenIdDictOptions> options)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task SeedApplicationConfiguration()
    {
        //For local
        //try
        //{
        //    var applicationConfig = await _manager.FindByClientIdAsync(_options.ClientId);
        //    if(applicationConfig != null)
        //    {
        //        await _manager.DeleteAsync(applicationConfig);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex?.Message);
        //}

        var existingClientApp = await _manager.FindByClientIdAsync(_options.ClientId);
        if (existingClientApp == null)
        {
            await _manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
                Permissions =
                {
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.Prefixes.Scope + _options.Scope,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.Endpoints.Revocation,
                    OpenIddictConstants.Permissions.Endpoints.Introspection,
                }
            });
        }
    } 
}