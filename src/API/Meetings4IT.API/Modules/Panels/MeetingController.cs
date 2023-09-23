using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Panels.Application.Features.Meetings.Commands.CreateMeetingInvitation;
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

    [SwaggerOperation(Summary = "Declaring new meeting")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<int>), 200)]
    [HttpPost("[action]")]
    public async Task<IActionResult> DeclareNewMeeting([FromBody] DeclareNewMeetingParameters parameters)
    {
        var response = await CommandBus.Send(new DeclareNewMeetingCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Creating new meeting invitation")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<int>), 200)]
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateNewMeetingInvitation([FromBody] CreateMeetingInvitationParameters parameters)
    {
        var response = await CommandBus.Send(new CreateMeetingInvitationCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Get meeting by Id")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetMeetingById([FromQuery] int meetingId)
    {
        return Ok(new { Message = "OK" });
    }

    [SwaggerOperation(Summary = "Get invitation by code")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetInvitationByCode([FromQuery] int code)
    {
        return Ok(new { Message = "OK" });
    }
}