using Identities.Core.Models.Entities;
using Identities.Core.Models.Entities.OpenIdDictCustomEntities;
using Meetings4IT.Shared.Implementations.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;

namespace Identities.Core.Managers;

public class AuthorizationManager : OpenIddictAuthorizationManager<CustomAuthorizationEntity>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthorizationManager(UserManager<ApplicationUser> userManager, IOpenIddictAuthorizationCache<CustomAuthorizationEntity> cache,
        ILogger<OpenIddictAuthorizationManager<CustomAuthorizationEntity>> logger,
        IOptionsMonitor<OpenIddictCoreOptions> options, IOpenIddictAuthorizationStoreResolver resolver) : base(cache,
        logger, options, resolver)
    {
        _userManager = userManager;
    }

    public override ValueTask PopulateAsync(CustomAuthorizationEntity authorization, OpenIddictAuthorizationDescriptor descriptor,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var userName = descriptor.Principal?.Claims.FirstOrDefault(_ => _.Type == MeetingsClaimTypes.UserName)?.Value;
        if (userName != null)
        {
            var user = _userManager?.FindByNameAsync(userName).Result;
            if (user != null)
            {
                authorization.UserId = user.Id;
            }
        }

        return base.PopulateAsync(authorization, descriptor, cancellationToken);
    }
}