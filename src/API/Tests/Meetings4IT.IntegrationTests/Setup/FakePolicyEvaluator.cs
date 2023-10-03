﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace Meetings4IT.IntegrationTests.Setup;

public class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var claimsPrincipal = UserSetup.UserPrincipals();

        var authTicket = new AuthenticationTicket(claimsPrincipal, "ticketTest");
        var result = AuthenticateResult.Success(authTicket);
        return Task.FromResult(result);
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy,
        AuthenticateResult authenticationResult, HttpContext context,
        object? resource)
    {
        var result = PolicyAuthorizationResult.Success();

        return Task.FromResult(result);
    }
}