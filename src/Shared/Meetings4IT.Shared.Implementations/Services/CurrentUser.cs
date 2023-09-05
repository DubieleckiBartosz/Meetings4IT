using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Constants;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace Meetings4IT.Shared.Implementations.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    private ClaimsPrincipal? Claims => _httpContextAccessor?.HttpContext?.User;
    private List<Claim>? Roles => Claims?.Claims.Where(_ => _.Type == MeetingsClaimTypes.Role).ToList();
    public bool IsInRole(string roleName)
    {
        var resultRoles = Roles;
        var response = resultRoles?.Any(_ => _.Value == roleName);
        return response ?? false;
    }

    public List<string>? AvailableRoles() => Roles?.Select(_ => _.Value).ToList();

    public bool IsInRoles(string[] roles)
    {
        var resultRoles = Roles;
        var response = resultRoles?.Any(_ => roles.Contains(_.Value));
        return response ?? false;
    }

    public bool IsAdmin => IsInRole("Admin");

    public int UserId
    {
        get
        {
            var result = Claims?.Claims.FirstOrDefault(_ => _.Type == MeetingsClaimTypes.NameIdentifier)?.Value;
            if (result == null)
            {
                return default;
            }

            return int.TryParse(result, out var identifier) ? identifier : default;
        }
    }

    public string UserName
    {
        get
        {
            var result = Claims?.Claims.FirstOrDefault(_ => _.Type == MeetingsClaimTypes.UserName)?.Value;
            if (result == null)
            {
                throw new BaseException("User name cannot be null", "User name is null",
                    HttpStatusCode.Unauthorized);
            }

            return result;
        }
    }
    public string Email
    {
        get
        {
            var result = Claims?.Claims.FirstOrDefault(_ => _.Type == MeetingsClaimTypes.Email)?.Value;
            if (result == null)
            {
                throw new BaseException("User mail cannot be null", "User mail is null",
                    HttpStatusCode.Unauthorized);
            }

            return result;
        }
    }
}