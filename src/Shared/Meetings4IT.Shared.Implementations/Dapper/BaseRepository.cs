using System.Data;
using Dapper;
using Serilog;

namespace Meetings4IT.Shared.Implementations.Dapper;

public abstract class BaseRepository
{
    private readonly DapperConnection _connection;

    protected BaseRepository(string dbConnection, ILogger logger)
    {
        _connection = new DapperConnection(dbConnection, logger);
    }

    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null,
        CommandType? commandType = null, IDbTransaction? transaction = null)
    {
        return await _connection.WithConnection(
            async _ => await _.QueryAsync<T>(sql, param,
                commandType: commandType, transaction: transaction));
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbTransaction? transaction = null)
    {
        return await _connection.WithConnection(
            async _ => await _.QueryAsync(sql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction));
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql,
        Func<T1, T2, T3, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbTransaction? transaction = null)
    {
        return await _connection.WithConnection(
            async _ => await _.QueryAsync(sql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction));
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(string sql,
        Func<T1, T2, T3, T4, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbTransaction? transaction = null)
    {
        return await _connection.WithConnection(
            async _ => await _.QueryAsync(sql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction));
    }

    protected async Task<int> ExecuteAsync(string sql, object? param = null,
        CommandType? commandType = null, IDbTransaction? transaction = null)
    {
        return await _connection.WithConnection(
            async _ => await _.ExecuteAsync(sql, param,
                commandType: commandType, transaction: transaction), transaction);
    }
}