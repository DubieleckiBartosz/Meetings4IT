namespace Meetings4IT.Shared.Implementations.Mediator;

public interface ICommandBus
{
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default(CancellationToken)); 
}