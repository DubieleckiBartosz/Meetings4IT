namespace Meetings4IT.Shared.Abstractions.Kernel;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}