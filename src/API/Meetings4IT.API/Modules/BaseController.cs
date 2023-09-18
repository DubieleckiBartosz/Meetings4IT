using Meetings4IT.Shared.Implementations.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Meetings4IT.API.Modules;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly ICommandBus CommandBus;
    protected readonly IQueryBus QueryBus;

    public BaseController(ICommandBus commandBus, IQueryBus queryBus)
    {
        CommandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        QueryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
    }
}