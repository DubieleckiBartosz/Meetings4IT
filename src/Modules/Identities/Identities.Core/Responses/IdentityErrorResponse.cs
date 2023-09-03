namespace Identities.Core.Responses;

public class IdentityErrorResponse
{
    public string Code { get; }
    public string Description { get; }

    public IdentityErrorResponse(string code, string description)
    {
        Code = code;
        Description = description;
    }
}