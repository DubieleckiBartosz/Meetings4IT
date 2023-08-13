using MediatR;

namespace Meetings4IT.Shared.Implementations.Mediator;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}