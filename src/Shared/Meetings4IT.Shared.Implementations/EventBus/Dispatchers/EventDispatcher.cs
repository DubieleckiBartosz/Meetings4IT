using MediatR;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using Microsoft.Extensions.DependencyInjection;

namespace Meetings4IT.Shared.Implementations.EventBus.Dispatchers;

/*
  EventDispatcher must be registered as singleton because we use it in background service.
  We inject IServiceProvider here because we rely on scoped services
  https://learn.microsoft.com/en-us/dotnet/core/extensions/scoped-service?pivots=dotnet-7-0
*/

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync(CancellationToken cancellationToken = default, params IntegrationEvent[] events)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var scopedProcessingService =
                scope.ServiceProvider.GetRequiredService<IMediator>();
            foreach (var @event in @events)
            {
                await scopedProcessingService.Publish(@event, cancellationToken);
            }
        }
    }
}