using Dapper;
using Meetings4IT.Shared.Implementations.Polly;
using Polly;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace Meetings4IT.Shared.Implementations.Dapper;

//If we want, we can implement a resiliency connection in the future
//https://concurrentflows.com/basic-dapper-resiliency-using-polly
public abstract class BaseRepository
{
    private readonly string _connectionString;
    private readonly ILogger _logger;
    private readonly AsyncPolicy _retryAsyncPolicyQuery;
    private readonly AsyncPolicy _retryAsyncPolicyConnection;

    protected BaseRepository(string dbConnection, ILogger logger)
    {
        _connectionString = dbConnection ??
                            throw new ArgumentNullException(nameof(dbConnection));

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        var policy = new PolicySetup();
        _retryAsyncPolicyConnection = policy.PolicyConnectionAsync(_logger);
        _retryAsyncPolicyQuery = policy.PolicyQueryAsync(_logger);
    }

    public async Task<T> WithConnection<T>(
        Func<IDbConnection, Task<T>> funcData,
        IDbTransaction? transaction = null,
        IDbConnection? dbConnection = null)
    {
        try
        {
            SqlConnection? connection;

            if (transaction != null)
            {
                connection = transaction.Connection as SqlConnection;
                return await _retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection!));
            }

            if (dbConnection != null)
            {
                return await _retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(dbConnection!));
            }

            await using (connection = new SqlConnection(_connectionString))
            {
                await _retryAsyncPolicyConnection.ExecuteAsync(async () => await connection.OpenAsync());

                _logger.Information("Connection has been opened");

                return await _retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection));
            }
        }
        catch (TimeoutException ex)
        {
            throw new Exception($"Timeout sql exception: {ex}");
        }
        catch (SqlException ex)
        {
            throw new Exception($"Sql exception: {ex}");
        }
    }

    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.QueryAsync<T>(sql, param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.QueryAsync(sql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql,
        Func<T1, T2, T3, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.QueryAsync(sql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(string sql,
        Func<T1, T2, T3, T4, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.QueryAsync(sql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<int> ExecuteAsync(string sql, object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.ExecuteAsync(sql, param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }
}