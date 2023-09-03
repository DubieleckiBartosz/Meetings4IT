using Newtonsoft.Json;

namespace Identities.Core.Models.Parameters;

public class ForgotPasswordParameters
{
    public string Email { get; init; }

    public ForgotPasswordParameters()
    {
    }

    [JsonConstructor]
    public ForgotPasswordParameters(string email)
    {
        Email = email;
    }
}