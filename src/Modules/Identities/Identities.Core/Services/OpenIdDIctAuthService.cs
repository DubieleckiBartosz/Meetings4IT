using Identities.Core.Interfaces.Services;
using Identities.Core.Models.Entities.OpenIdDictCustomEntities;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Core;

namespace Identities.Core.Services;

public class OpenIdDIctAuthService : IOpenIdDIctAuthService
{
    private readonly OpenIddictAuthorizationManager<CustomAuthorizationEntity> _openIddictAuthorizationManager;
    private readonly OpenIddictTokenManager<CustomTokenEntity> _openIddictTokenManager;

    public OpenIdDIctAuthService(
        OpenIddictAuthorizationManager<CustomAuthorizationEntity> openIddictAuthorizationManager,
        OpenIddictTokenManager<CustomTokenEntity> openIddictTokenManager)
    {
        _openIddictAuthorizationManager = openIddictAuthorizationManager;
        _openIddictTokenManager = openIddictTokenManager;
    }

    public async Task DeleteAllAuthorizationsForUser(string userId, CancellationToken cancellationToken = default)
    {
        IQueryable<CustomAuthorizationEntity> QueryFunctionAuthorization(IQueryable<CustomAuthorizationEntity> authorizations) =>
            authorizations.Where(authorization => authorization.UserId == userId).Include(_ => _.Tokens);

        var authorizations = await _openIddictAuthorizationManager
            .ListAsync(QueryFunctionAuthorization)
            .ToListAsync(cancellationToken);

        foreach (var authorization in authorizations)
        {
            await _openIddictAuthorizationManager.DeleteAsync(authorization, cancellationToken);
        }
    }
}