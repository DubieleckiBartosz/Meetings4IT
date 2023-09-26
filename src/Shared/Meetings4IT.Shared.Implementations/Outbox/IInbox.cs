namespace Meetings4IT.Shared.Implementations.Outbox;

//Same as in the outbox
public interface IInbox
{
    Task HandleAsync(Guid messageId, string name, Func<Task> handler);
}