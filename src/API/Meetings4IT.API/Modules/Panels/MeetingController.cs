using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Panels.Application.Features.Meetings.Commands.AcceptMeetingInvitation;
using Panels.Application.Features.Meetings.Commands.AddMeetingComment;
using Panels.Application.Features.Meetings.Commands.CancelMeeting;
using Panels.Application.Features.Meetings.Commands.CreateMeetingInvitation;
using Panels.Application.Features.Meetings.Commands.DeclareNewMeeting;
using Panels.Application.Features.Meetings.Commands.DeleteInvitationRequest;
using Panels.Application.Features.Meetings.Commands.DeleteMeetingComment;
using Panels.Application.Features.Meetings.Commands.RejectInvitationRequest;
using Panels.Application.Features.Meetings.Commands.RejectMeetingInvitation;
using Panels.Application.Features.Meetings.Commands.UpdateMeetingComment;
using Panels.Application.Features.Meetings.Queries.GetMeetingsBySearch;
using Panels.Application.Models.Parameters.MeetingParams;
using Panels.Application.Models.Views;
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

    [SwaggerOperation(Summary = "Canceling meeting")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPut("[action]")]
    public async Task<IActionResult> CancelMeeting([FromBody] CancelMeetingParameters parameters)
    {
        var response = await CommandBus.Send(new CancelMeetingCommand(parameters));
        return Ok(response);
    }

    [AllowAnonymous]
    [SwaggerOperation(Summary = "Accepting invitation")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPut("[action]")]
    public async Task<IActionResult> AcceptInvitation([FromBody] AcceptMeetingInvitationParameters parameters)
    {
        var response = await CommandBus.Send(new AcceptMeetingInvitationCommand(parameters));
        return Ok(response);
    }

    [AllowAnonymous]
    [SwaggerOperation(Summary = "Rejection invitation")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPut("[action]")]
    public async Task<IActionResult> RejectInvitation([FromBody] RejectMeetingInvitationParameters parameters)
    {
        var response = await CommandBus.Send(new RejectMeetingInvitationCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Rejection invitation request")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPut("[action]")]
    public async Task<IActionResult> RejectInvitationRequest([FromBody] RejectInvitationRequestParameters parameters)
    {
        var response = await CommandBus.Send(new RejectInvitationRequestCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Delete invitation request")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteInvitationRequest([FromBody] DeleteInvitationRequestParameters parameters)
    {
        var response = await CommandBus.Send(new DeleteInvitationRequestCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Adding new meeting comment")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddMeetingComment([FromBody] AddMeetingCommentParameters parameters)
    {
        var response = await CommandBus.Send(new AddMeetingCommentCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Updating a meeting comment")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateMeetingComment([FromBody] UpdateMeetingCommentParameters parameters)
    {
        var response = await CommandBus.Send(new UpdateMeetingCommentCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Delete a meeting comment")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteMeetingComment([FromBody] DeleteMeetingCommentParameters parameters)
    {
        var response = await CommandBus.Send(new DeleteMeetingCommentCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Get meetings by search")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<List<MeetingSearchViewModel>>), 200)]
    [HttpPost("[action]")]
    public async Task<IActionResult> GetMeetingsBySearch([FromBody] GetMeetingsBySearchParameters parameters)
    {
        var response = await QueryBus.Send(new GetMeetingsBySearchQuery(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Get meeting by Id")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetMeetingById([FromRoute] int meetingId)
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