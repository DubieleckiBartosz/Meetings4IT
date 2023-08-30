using Meetings4IT.Shared.Implementations.EventBus.Channel;
using Meetings4IT.Shared.Implementations.EventBus.IntegrationEventProcess;
using System.Text;
using Meetings4IT.Shared.Implementations.Tools;

namespace Meetings4IT.Shared.Implementations.EventBus.Dispatchers;

public class AsyncEventDispatcher : IAsyncEventDispatcher
{
    private readonly IEventChannel _channel;

    public AsyncEventDispatcher(IEventChannel channel)
    {
        _channel = channel;
    }

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IntegrationEvent
    {
        var navigator = message.GetType().Name!; 
        var body = Encoding.UTF8.GetBytes(message.Serialize());

        var messageChannel = new MessageChannel(body, navigator);
        await _channel.Writer.WriteAsync(messageChannel, cancellationToken);
    }
}