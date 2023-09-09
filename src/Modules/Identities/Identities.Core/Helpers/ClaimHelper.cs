using Identities.Core.Models.Entities;
using Meetings4IT.Shared.Implementations.Constants;
using OpenIddict.Abstractions;
using System.Collections.Immutable;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identities.Core.Helpers;

public static class ClaimHelper
{
    public static ClaimsPrincipal SetUserClaims(
        this ClaimsIdentity identity,
        ApplicationUser user,
        ImmutableArray<string> roles,
        ImmutableArray<string> scopes)
    {
        identity
            .SetClaim(MeetingsClaimTypes.Subject, user.Id)
            .SetClaim(MeetingsClaimTypes.Email, user.Email)
            .SetClaim(MeetingsClaimTypes.UserName, user.UserName)
            .SetClaims(MeetingsClaimTypes.Role, roles);

        identity.SetScopes(scopes);
        //identity.SetDestinations(GetDestinations);
        identity.SetDestinations(claim => new[] { Destinations.AccessToken, Destinations.IdentityToken });

        return new ClaimsPrincipal(identity);
    }

    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        switch (claim.Type)
        {
            case MeetingsClaimTypes.UserName:
                {
                    yield return Destinations.AccessToken;

                    if (claim.Subject!.HasScope(Scopes.Profile))
                        yield return Destinations.IdentityToken;

                    yield break;
                }
            case MeetingsClaimTypes.Email:
                {
                    yield return Destinations.AccessToken;

                    if (claim.Subject!.HasScope(Scopes.Email))
                        yield return Destinations.IdentityToken;

                    yield break;
                }
            case MeetingsClaimTypes.Role:
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