using System.Data;

namespace Meetings4IT.Shared.Implementations.Dapper;

public interface ITransactionSupervisor : IDisposable
{
    Guid TransactionId { get; }

    IDbTransaction? GetTransactionWhenExist();

    Task<IDbTransaction> GetOpenOrCreateTransaction();

    bool Commit();

    void Rollback();
}