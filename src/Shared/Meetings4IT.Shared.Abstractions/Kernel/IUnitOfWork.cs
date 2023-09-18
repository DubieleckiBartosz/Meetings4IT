namespace Meetings4IT.Shared.Abstractions.Kernel;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
}