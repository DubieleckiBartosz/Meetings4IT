using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Core.Handlers.Alerts.Commands.ReadAlerts;
using Notifications.Core.Handlers.Alerts.Queries;
using Notifications.Core.Models.Parameters;
using Notifications.Core.Models.Views;
using OpenIddict.Validation.AspNetCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Meetings4IT.API.Modules.Notifications;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class AlertController : BaseController
{
    public AlertController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }

    [SwaggerOperation(Summary = "Reading alerts")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [HttpPut("[action]")]
    public async Task<IActionResult> ReadAlerts([FromBody] ReadAlertsParameters parameters)
    {
        var response = await CommandBus.Send(new ReadAlertsCommand(parameters));
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Returns a list of the user's current not unread alerts")]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<List<AlertViewModel>?>), 200)]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetCurrentUserUnreadedAlerts()
    {
        var response = await QueryBus.Send(new UnreadedAlertsQuery());
        return Ok(response);
    }
}