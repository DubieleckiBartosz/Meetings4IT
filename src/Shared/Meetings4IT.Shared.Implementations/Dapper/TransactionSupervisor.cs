using Polly;
using System.Data.SqlClient;
using System.Data;
using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Polly;
using Serilog;
using Microsoft.Extensions.Options;
using Meetings4IT.Shared.Implementations.Options;

namespace Meetings4IT.Shared.Implementations.Dapper;

public class TransactionSupervisor : ITransactionSupervisor, IDisposable 
{
    private readonly ILogger _logger;
    private readonly string _connectionString;
    private readonly AsyncPolicy _retryAsyncPolicyConnection;

    private string? _transactionId; 
    private SqlConnection? _connection;
    private SqlTransaction? _transaction;
    private bool _disposed = false;

    public TransactionSupervisor(ILogger logger, IOptions<DatabaseOptions> dbConnectionOptions)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._connectionString = dbConnectionOptions?.Value?.DefaultConnection ?? throw new ArgumentNullException(nameof(dbConnectionOptions));
        var policy = new PolicySetup();
        this._retryAsyncPolicyConnection = policy.PolicyConnectionAsync(this._logger);
    }

    public async Task<IDbTransaction?> GetOpenOrCreateTransaction()
    {
        try
        {
            if (this._transaction != null)
            {
                return this._transaction;
            }

            this.TransactionIdGenerator();

            if (this._connection != null)
            {
                this._transaction = this._connection.BeginTransaction();
                return this._transaction;
            }

            this._connection = new SqlConnection(this._connectionString);
            await this._retryAsyncPolicyConnection.ExecuteAsync(async () => await this._connection.OpenAsync());
            this._transaction = this._connection.BeginTransaction();
            return this._transaction;
        }
        catch (Exception ex)
        {
            throw new BaseException("CreateTransaction method exception", $"{ex?.InnerException}");
        }
    }

    public IDbTransaction? GetTransactionWhenExist()
    {
        return this._transaction;
    }

    public void Rollback()
    {
        try
        {
            if (_transaction != null)
            {
                this._logger.Information("Rollback start.");
                this._transaction?.Rollback();
                this._logger.Information("Rollback OK."); 
            }
        }
        catch
        {
            this._logger.Error("Rollback failed.");
            throw;
        }
        finally
        {
            if (this._transaction != null)
            {
                this._transaction.Dispose();
                this._transaction?.Connection?.Dispose();
                this._transaction = null;
            }
        }
    }

    public bool Commit()
    {
        try
        {
            if (this._transaction == null)
            {
                this._logger.Information("Transaction is null.");
                return false;
            }

            this._transaction.Commit();
            this._logger.Information(
                $"Transaction committed successfully ------ TRANSACTION ------- : {this._transactionId}");
            return true;
        }
        catch
        {
            _logger.Error("Commit failed.");
            this.Rollback();
            throw;
        }
        finally
        {
            if (this._transaction != null)
            {
                this._transaction.Dispose();
                this._transaction?.Connection?.Dispose();
                this._transaction = null;
            }
        }
    }

    private void TransactionIdGenerator()
    {
        this._transactionId = Guid.NewGuid().ToString();
        this._logger.Information($"New transaction identifier created: {this._transactionId}");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources here
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
             
            _disposed = true;
        }
    }
}