using MediatR;
using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Tools;
using Serilog;

namespace Meetings4IT.Shared.Implementations.Behaviours;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IRequestHandler<TRequest, TResponse> _requestHandler;
    private readonly ILogger _logger;
    private readonly ITransactionSupervisor _transaction;

    public TransactionBehavior(IRequestHandler<TRequest, TResponse> requestHandler,
        ILogger logger, ITransactionSupervisor transaction)
    {
        this._requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var hasTransactionAttribute = this._requestHandler.GetType()
            .GetCustomAttributes(typeof(WithTransactionAttribute), false).Any();

        if (!hasTransactionAttribute)
        {
            return await next();
        }
        else
        {
            try
            {
                var requestName = request.GetType().FullName;
                this._logger.Information(
                    $"The transaction will be created by {requestName} ------ HANDLER WITH TRANSACTION ------- ");

                await _transaction.GetOpenOrCreateTransaction();

                var response = await next();

                var result = this._transaction.Commit();

                if (result)
                {
                    this._logger.Information(
                        $"Committed transaction {requestName} ------ COMMITTED TRANSACTION IN HANDLER ------- ");
                }

                return response;
            }
            catch (Exception ex)
            {
                this._transaction.Rollback();

                var message = new
                {
                    Message = "ERROR Handling transaction.",
                    DataException = ex,
                    Request = request,
                };

                this._logger.Error(message.Serialize());

                throw;
            }
        }
    }
}