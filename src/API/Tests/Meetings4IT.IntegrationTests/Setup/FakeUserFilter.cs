using Microsoft.AspNetCore.Mvc.Filters;

namespace Meetings4IT.IntegrationTests.Setup;

public class FakeUserFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.User = UserSetup.UserPrincipals();

        await next();
    }
}