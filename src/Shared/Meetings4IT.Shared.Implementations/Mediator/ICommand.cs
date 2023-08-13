using MediatR;

namespace Meetings4IT.Shared.Implementations.Mediator;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}