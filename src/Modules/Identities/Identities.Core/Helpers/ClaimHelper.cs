using System.Collections.Immutable;
using System.Security.Claims;
using Identities.Core.Models.DataTransferObjects;
using Meetings4IT.Shared.Implementations.Constants; 
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identities.Core.Helpers;

public static class ClaimHelper
{
    public static ClaimsPrincipal SetUserClaims(this ClaimsIdentity identity, 
        UserDto user, 
        ImmutableArray<string> roles,
        ImmutableArray<string> scopes)
    {
        identity.SetClaim(MeetingsClaimTypes.Subject, user.Id)//czy sub siedzi w tokenie??
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
            case MeetingsClaimTypes.UserName:
                yield return Destinations.AccessToken;

                if (claim.Subject.HasScope(Scopes.Profile))
                {
                    yield return Destinations.IdentityToken;
                }
                break;

            case MeetingsClaimTypes.Email:
                yield return Destinations.AccessToken;

                if (claim.Subject.HasScope(Scopes.Email))
                {
                    yield return Destinations.IdentityToken;
                }
                break;

            case MeetingsClaimTypes.Role:
                yield return Destinations.AccessToken;

                if (claim.Subject.HasScope(Scopes.Roles))
                {
                    yield return Destinations.IdentityToken;
                }
                break;

            // Never include the security stamp in the access and identity tokens, as it's a secret value.
            case "AspNet.Identity.SecurityStamp":
                break;

            default:
                yield return Destinations.AccessToken;
                break;
        }
    }
}