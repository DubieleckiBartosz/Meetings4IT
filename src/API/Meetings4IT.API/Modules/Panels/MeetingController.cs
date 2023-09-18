using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Panels.Application.Features.Meetings.Commands.DeclareNewMeeting;
using Panels.Application.Models.Parameters;
using Swashbuckle.AspNetCore.Annotations;

namespace Meetings4IT.API.Modules.Panels;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class MeetingController : BaseController
{
    public MeetingController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }

    [SwaggerOperation(Summary = "Creating new meeting")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<int>), 200)]
    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> GetProductById([FromBody] DeclareNewMeetingParameters parameters)
    {
        var response = await CommandBus.Send(new DeclareNewMeetingCommand(parameters));
        return Ok(response);
    }
}