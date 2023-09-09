using Identities.Core.Options;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identities.Core.DAL.Seed;

public class OpenIdAppConfigSeeder
{
    private readonly IOpenIddictApplicationManager _manager;
    private readonly IOpenIddictScopeManager _openIddictScopeManager;
    private readonly OpenIdDictOptions _options;

    public OpenIdAppConfigSeeder(IOpenIddictApplicationManager manager, IOptions<OpenIdDictOptions> options, IOpenIddictScopeManager openIddictScopeManager)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _openIddictScopeManager = openIddictScopeManager ?? throw new ArgumentNullException(nameof(openIddictScopeManager));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task SeedApplicationConfiguration()
    {
        //For local
        //try
        //{
        //    var applicationConfig = await _manager.FindByClientIdAsync(_options.ClientId);
        //    if (applicationConfig != null)
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
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.GrantTypes.Password,
                    Permissions.Endpoints.Token,
                    Permissions.Endpoints.Revocation,
                    Permissions.Endpoints.Introspection,
                    Permissions.Prefixes.Scope + _options.Scope,
                },
            });
        }

        if (await _openIddictScopeManager.FindByNameAsync(_options.Scope) is null)
        {
            await _openIddictScopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                DisplayName = "Meetings API access",
                Name = _options.Scope,
                Resources =
                    {
                       _options.ClientId
                    }
            });
        }
    }
}