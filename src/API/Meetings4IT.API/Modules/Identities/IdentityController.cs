using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Meetings4IT.API.Modules.Identities;

[Route("api/[controller]")]
[ApiController] 
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class IdentityController : ControllerBase
{
     
}