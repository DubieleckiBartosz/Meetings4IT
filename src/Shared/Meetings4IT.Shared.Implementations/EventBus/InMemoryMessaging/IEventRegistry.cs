namespace Meetings4IT.Shared.Implementations.EventBus.InMemoryMessaging;

public interface IEventRegistry
{
    void Register(string key, Type type);
    Type Navigate(string key);
}