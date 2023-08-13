using MediatR;

namespace Meetings4IT.Shared.Implementations.Mediator;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}