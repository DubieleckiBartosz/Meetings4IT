using MediatR;
using Meetings4IT.Shared.Implementations;
using Meetings4IT.Shared.Implementations.EventBus.Dispatchers;
using Meetings4IT.Shared.Implementations.EventBus.Messaging;
using Meetings4IT.Shared.Implementations.Extensions;
using Meetings4IT.Shared.Implementations.Wrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Panels.Application.Features.Users.Commands.CreateNewUser;

namespace Panels.Application.Configurations;

public static class EventPanelInitiator
{
    public static WebApplication InitializePanelEvents(this WebApplication app)
    {
        app.RegisterClient().RegisterEvents();

        return app;
    }

    private static WebApplication RegisterClient(this WebApplication app)
    {
        app.UseModuleRequests()
            .Subscribe<CreateNewUserCommand, Response>("user/create",
                (command, serviceProvider, cancellationToken)
                    => serviceProvider.GetRequiredService<IMediator>().Send(command, cancellationToken));
        return app;
    }

    private static WebApplication RegisterEvents(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IEventRegistry>();
        typeof(PanelAssemblyReference).Assembly.RegistrationAssemblyIntegrationEvents(service);

        return app;
    }
}