using Polly;
using System.Data;
using System.Data.SqlClient;
using Meetings4IT.Shared.Implementations.Polly;
using Serilog;

namespace Meetings4IT.Shared.Implementations.Dapper;

public class DapperConnection
{
    private readonly string _connectionString;
    private readonly ILogger _logger;
    private readonly AsyncPolicy _retryAsyncPolicyQuery;
    private readonly AsyncPolicy _retryAsyncPolicyConnection;
    public DapperConnection(string connectionString, ILogger logger)
    {
        _connectionString = connectionString;
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        var policy = new PolicySetup();
        this._retryAsyncPolicyConnection = policy.PolicyConnectionAsync(this._logger);
        this._retryAsyncPolicyQuery = policy.PolicyQueryAsync(this._logger);
    }

    public async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> funcData, IDbTransaction? transaction = null)
    {
        try
        {
            SqlConnection? connection;

            if (transaction != null)
            {
                connection = transaction.Connection as SqlConnection;
                return await this._retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection!));
            }

            await using (connection = new SqlConnection(this._connectionString))
            {
                await this._retryAsyncPolicyConnection.ExecuteAsync(async () => await connection.OpenAsync());

                _logger.Information("Connection has been opened");

                return await this._retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection));
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