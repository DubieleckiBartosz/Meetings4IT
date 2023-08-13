using MediatR;

namespace Meetings4IT.Shared.Implementations.Mediator;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}