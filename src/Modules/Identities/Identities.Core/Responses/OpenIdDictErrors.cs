namespace Identities.Core.Responses;

public class OpenIdDictErrors
{
    public const string GrantTypeNotImplemented = "The specified grant type is not implemented.";

    public static Dictionary<string, string> ErrorWhenInvalidUser() => new Dictionary<string, string>
    {
        [".error"] = "invalid_grant",
        [".error_description"] =
            "The userName/password couple is invalid."
    };

    public static Dictionary<string, string> ErrorWhenUserIsNull() => new Dictionary<string, string>
    {
        [".error"] = "invalid_grant",
        [".error_description"] =
            "The refresh token is no longer valid."
    };

    public static Dictionary<string, string> ErrorWhenUserIsNoLongerAllowed() => new Dictionary<string, string>
    {
        [".error"] = "invalid_grant",
        [".error_description"] = "The user is no longer allowed to sign in."
    };
}