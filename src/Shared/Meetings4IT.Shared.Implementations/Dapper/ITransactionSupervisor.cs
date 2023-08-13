using System.Data;

namespace Meetings4IT.Shared.Implementations.Dapper;

public interface ITransactionSupervisor
{
    IDbTransaction? GetTransactionWhenExist();
    Task<IDbTransaction?> GetOpenOrCreateTransaction();
    bool Commit();
    void Rollback();
}