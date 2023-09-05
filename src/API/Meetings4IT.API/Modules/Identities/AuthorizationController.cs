using Identities.Core.Helpers;
using Identities.Core.Interfaces.Services;
using Identities.Core.Responses;
using Meetings4IT.Shared.Implementations.Constants;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Collections.Immutable;
using System.Security.Claims;

namespace Meetings4IT.API.Modules.Identities;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IUserQueryService _userService;

    public AuthorizationController(IUserQueryService userService)
    {
        _userService = userService;
    }

    [HttpPost("~/connect/token")]
    [IgnoreAntiforgeryToken]
    [Produces("application/json")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ?? throw new AbandonedMutexException("Request cannot be null");
        if (request.IsPasswordGrantType())
        {
            var user = await _userService.GetUserByNameAsync(request.Username!);
            if (user == null)
            {
                var properties = new AuthenticationProperties(OpenIdDictErrors.ErrorWhenInvalidUser()!);
                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var blocked = await _userService.UserIsBlockedAsync(user, request.Password!);
            if (blocked)
            {
                var properties = new AuthenticationProperties(OpenIdDictErrors.ErrorWhenInvalidUser()!);
                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var roles = await _userService.GetUserRolesByUserAsync(user);
            var scopes = request.GetScopes();

            var identity = new ClaimsIdentity(
                TokenValidationParameters.DefaultAuthenticationType,
                MeetingsClaimTypes.Email,
                MeetingsClaimTypes.Role);

            var claimsPrincipal = identity.SetUserClaims(user, roles.ToImmutableArray(), scopes);

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        if (request.IsRefreshTokenGrantType())
        {
            var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            //sub claim is required
            var sub = result.Principal!.GetClaim(MeetingsClaimTypes.Subject)!;

            var user = await _userService.GetUserByIdAsync(sub);

            if (user == null)
            {
                var properties = new AuthenticationProperties(OpenIdDictErrors.ErrorWhenUserIsNull()!);

                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            if (!await _userService.UserIsStillAllowedToSignInAsync(user))
            {
                var properties = new AuthenticationProperties(OpenIdDictErrors.ErrorWhenUserIsNoLongerAllowed()!);

                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var roles = await _userService.GetUserRolesByUserAsync(user);
            var scopes = request.GetScopes();

            var identity = new ClaimsIdentity(result.Principal!.Claims,
                TokenValidationParameters.DefaultAuthenticationType,
                MeetingsClaimTypes.Email,
                MeetingsClaimTypes.Role);

            var claimsPrincipal = identity.SetUserClaims(user, roles.ToImmutableArray(), scopes);

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        throw new NotImplementedException(OpenIdDictErrors.GrantTypeNotImplemented);
    }
}