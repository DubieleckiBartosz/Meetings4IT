using Dapper;
using System.Data;

namespace Meetings4IT.Shared.Implementations.Dapper;

public abstract class BaseRepository<TContext> where TContext : DapperContext
{
    private readonly TContext _dapperContext;

    protected BaseRepository(TContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await _dapperContext.WithConnection(
            async _ => await _.QueryAsync<T>(sql, param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await _dapperContext.WithConnection(
            async _ => await _.QueryAsync(sql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql,
        Func<T1, T2, T3, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await _dapperContext.WithConnection(
            async _ => await _.QueryAsync(sql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(string sql,
        Func<T1, T2, T3, T4, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await _dapperContext.WithConnection(
            async _ => await _.QueryAsync(sql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<int> ExecuteAsync(string sql, object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await _dapperContext.WithConnection(
            async _ => await _.ExecuteAsync(sql, param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }
}