using Meetings4IT.IntegrationTests.Constants;
using Meetings4IT.Shared.Implementations.Constants;
using System.Security.Claims;

namespace Meetings4IT.IntegrationTests.Setup;

public class UserSetup
{
    public enum FakeRoles
    {
        Admin = 1,
        User = 2
    }

    public static ClaimsPrincipal UserPrincipals()
    {
        var claimsPrincipal = new ClaimsPrincipal();
        var userName = GlobalUserData.UserName;
        var userEmail = GlobalUserData.UserEmail;
        var sub = GlobalUserData.Identifier;

        var claims = new List<Claim>
        {
            new Claim(MeetingsClaimTypes.Subject, sub),
            new Claim(MeetingsClaimTypes.Role, FakeRoles.User.ToString()),
            new Claim(MeetingsClaimTypes.Role, FakeRoles.Admin.ToString()),
            new Claim(MeetingsClaimTypes.Email, userEmail),
            new Claim(MeetingsClaimTypes.UserName, userName)
        };

        claimsPrincipal.AddIdentity(new ClaimsIdentity(claims));

        return claimsPrincipal;
    }
}