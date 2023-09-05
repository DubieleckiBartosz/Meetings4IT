using Meetings4IT.Shared.Implementations.Tools;
using Polly;
using Serilog;
using System.Data.SqlClient;

namespace Meetings4IT.Shared.Implementations.Polly;

public class PolicySetup
{
    public PolicySetup()
    {
    }

    public AsyncPolicy PolicyConnectionAsync(ILogger logger) => Policy
        .Handle<SqlException>()
        .Or<TimeoutException>()
        .WaitAndRetryAsync(
            3,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (exception, timeSpan, retryCount, context) =>
            {
                logger?.Error(new
                {
                    RetryAttempt = retryCount,
                    ExceptionMessage = exception?.Message,
                    Waiting = timeSpan.Seconds
                }.Serialize());
            }
        );


    public AsyncPolicy PolicyQueryAsync(ILogger logger) => Policy.Handle<SqlException>()
        .WaitAndRetryAsync(
            3,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (exception, timeSpan, retryCount, context) =>
            {
                logger?.Error(new
                {
                    RetryAttempt = retryCount,
                    ExceptionMessage = exception?.Message,
                    ProcedureName = this.GetProcedure(exception),
                    Waiting = timeSpan.Seconds
                }.Serialize());
            }
        );


    #region Private

    private string GetProcedure(Exception? exception) => exception is SqlException ex ? ex.Procedure : string.Empty;

    #endregion
}