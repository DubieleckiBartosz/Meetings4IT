using Microsoft.AspNetCore.Http;
using Serilog;
using System.Security.Claims;

namespace Meetings4IT.Shared.Implementations.Logging;

public class LogDetails
{
    public static void ReadHttpRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        var httpContextInfo = new HttpContextLogInfo
        {
            Protocol = httpContext.Request.Protocol,
            Scheme = httpContext.Request.Scheme,
            IpAddress = httpContext.Connection?.RemoteIpAddress?.ToString(),
            Host = httpContext.Request.Host.ToString(),
            User = GetUserInfo(httpContext.User)
        };

        diagnosticContext.Set("HttpContext", httpContextInfo, true);
    }

    private static string GetUserInfo(ClaimsPrincipal user)
    {
        if (user.Identity is { IsAuthenticated: true })
        {
            return user.Identity.Name!;
        }

        return Environment.UserName;
    }
}