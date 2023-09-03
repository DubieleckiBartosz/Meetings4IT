using Meetings4IT.Shared.Implementations.Polly;
using Polly;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace Meetings4IT.Shared.Implementations.Dapper;

//If we want, we can implement a resiliency connection in the future
//https://concurrentflows.com/basic-dapper-resiliency-using-polly
public abstract class DapperContext
{
    private readonly string _connectionString;
    private readonly ILogger _logger;
    private readonly AsyncPolicy _retryAsyncPolicyQuery;
    private readonly AsyncPolicy _retryAsyncPolicyConnection;

    protected DapperContext(string dbConnection, ILogger logger)
    {
        _connectionString = dbConnection ??
                            throw new ArgumentNullException(nameof(dbConnection));

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        var policy = new PolicySetup();
        _retryAsyncPolicyConnection = policy.PolicyConnectionAsync(_logger);
        _retryAsyncPolicyQuery = policy.PolicyQueryAsync(_logger);
    }

    public async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> funcData, IDbTransaction? transaction = null,
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
}