namespace Meetings4IT.Shared.Implementations.EventBus.Channel;

public record MessageChannel(byte[] Body, string Navigator);