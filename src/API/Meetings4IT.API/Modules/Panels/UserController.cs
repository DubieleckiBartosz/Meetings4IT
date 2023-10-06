using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Panels.Application.Features.Users.Commands.AddNewUserOpinion;
using Panels.Application.Features.Users.Commands.CompleteUserDetails;
using Panels.Application.Features.Users.Commands.DeleteUserOpinion;
using Panels.Application.Features.Users.Commands.UpdateUserOpinion;
using Panels.Application.Models.Parameters.UserParams;
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
        var response = await CommandBus.Send(new CompleteUserDetailsCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Adding new user opinion")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddNewUserOpinion([FromBody] AddNewUserOpinionParameters parameters)
    {
        var response = await CommandBus.Send(new AddNewUserOpinionCommand(
            parameters.UserId, parameters.RatingSoftSkills, parameters.RatingTechSkills, parameters.Content));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Update user opinion")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPut("[action]")]
    public async Task<IActionResult> NewUserRatingTechnicalSkills([FromBody] UpdateUserOpinionParameters parameters)
    {
        var response = await CommandBus.Send(new UpdateUserOpinionCommand(
            parameters.UserId, parameters.OpinionId, parameters.RatingSoftSkills, parameters.RatingTechSkills, parameters.Content));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Deleting user opinion")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteOpinion([FromBody] DeleteUserOpinionParameters parameters)
    {
        var response = await CommandBus.Send(new DeleteUserOpinionCommand(parameters.UserId, parameters.OpinionId));
        return Ok(response);
    }
}