using System.Collections.Immutable;
using System.Security.Claims;
using Identities.Core.Models.DataTransferObjects;
using Meetings4IT.Shared.Implementations.Constants; 
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identities.Core.Helpers;

public static class ClaimHelper
{
    public static ClaimsPrincipal SetUserClaims(
        this ClaimsIdentity identity, 
        UserDto user, 
        ImmutableArray<string> roles,
        ImmutableArray<string> scopes)
    {
        identity
            .SetClaim(MeetingsClaimTypes.Subject, user.Id)
            .SetClaim(MeetingsClaimTypes.Email, user.Email)
            .SetClaim(MeetingsClaimTypes.UserName, user.UserName)
            .SetClaims(MeetingsClaimTypes.Role, roles);

        identity.SetScopes(scopes);
        identity.SetDestinations(GetDestinations);

        return new ClaimsPrincipal(identity);
    }

    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        switch (claim.Type)
        {
            case Claims.Name:
            {
                yield return Destinations.AccessToken;

                if (claim.Subject!.HasScope(Scopes.Profile))
                    yield return Destinations.IdentityToken;

                yield break;
            }
            case Claims.Email:
            {
                yield return Destinations.AccessToken;

                if (claim.Subject!.HasScope(Scopes.Email))
                    yield return Destinations.IdentityToken;

                yield break;
            }
            case Claims.Role:
            {
                yield return Destinations.AccessToken;

                if (claim.Subject!.HasScope(Scopes.Roles))
                    yield return Destinations.IdentityToken;

                yield break;
            }
            // Never include the security stamp in the access and identity tokens, as it's a secret value.
            case "AspNet.Identity.SecurityStamp": yield break;

            default:
                yield return Destinations.AccessToken;
                yield break;
        }
    }
}