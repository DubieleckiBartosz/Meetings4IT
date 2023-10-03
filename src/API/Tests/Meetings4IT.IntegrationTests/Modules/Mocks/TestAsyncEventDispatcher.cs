using Meetings4IT.Shared.Implementations.EventBus.Dispatchers;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;

namespace Meetings4IT.IntegrationTests.Modules.Mocks;

public class TestAsyncEventDispatcher : IAsyncEventDispatcher
{
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IntegrationEvent
    {
        await Task.CompletedTask;
    }
}