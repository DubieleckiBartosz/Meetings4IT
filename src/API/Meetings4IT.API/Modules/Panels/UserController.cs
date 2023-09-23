using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Panels.Application.Features.Users.Commands.CompleteUserDetails;
using Panels.Application.Models.Parameters;
using Swashbuckle.AspNetCore.Annotations;

namespace Meetings4IT.API.Modules.Panels;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    public UserController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }

    [SwaggerOperation(Summary = "Completing user details")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPost("[action]")]
    public async Task<IActionResult> CompleteUser([FromForm] CompleteUserDetailsParameters parameters)
    {
        await CommandBus.Send(new CompleteUserDetailsCommand(parameters));
        return Ok();
    }
}