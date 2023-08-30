using System.Threading.Channels;

namespace Meetings4IT.Shared.Implementations.EventBus.Channel;

public interface IEventChannel
{
    ChannelReader<MessageChannel> Reader { get; }
    ChannelWriter<MessageChannel> Writer { get; }
}