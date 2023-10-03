using Newtonsoft.Json;

namespace Identities.Core.Models.Parameters;

public class ResetPasswordParameters
{
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
    public string Email { get; init; }
    public string Token { get; init; }

    public ResetPasswordParameters()
    {
    }

    [JsonConstructor]
    public ResetPasswordParameters(string password, string confirmPassword, string email, string token)
    {
        Password = password;
        ConfirmPassword = confirmPassword;
        Email = email;
        Token = token;
    }
}