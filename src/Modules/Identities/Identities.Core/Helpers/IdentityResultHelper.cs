using Identities.Core.Responses;
using Microsoft.AspNetCore.Identity;

namespace Identities.Core.Helpers;

public static class IdentityResultHelper
{
    public static List<IdentityErrorResponse> ReadResult(this IdentityResult result)
    {
        var errors = new List<IdentityErrorResponse>();
        foreach (var error in result.Errors)
        {
            errors.Add(new IdentityErrorResponse(error.Code, error.Description));
        }

        return errors;
    }
}