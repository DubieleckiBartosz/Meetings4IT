using System.Threading.Channels;

namespace Meetings4IT.Shared.Implementations.EventBus.Channel;

public class EventChannel : IEventChannel
{
    private readonly Channel<MessageChannel> _messages =
        System.Threading.Channels.Channel.CreateUnbounded<MessageChannel>();

    public ChannelReader<MessageChannel> Reader => _messages.Reader;
    public ChannelWriter<MessageChannel> Writer => _messages.Writer;
}