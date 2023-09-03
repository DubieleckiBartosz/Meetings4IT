using MediatR;
using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Tools;
using Serilog;

namespace Meetings4IT.Shared.Implementations.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IRequestHandler<TRequest, TResponse> _requestHandler;
    private readonly ILogger _logger;
    private readonly ITransactionSupervisor _transactionSupervisor;

    public TransactionBehavior(IRequestHandler<TRequest, TResponse> requestHandler,
        ILogger logger, ITransactionSupervisor transactionSupervisor)
    {
        _requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _transactionSupervisor = transactionSupervisor ?? throw new ArgumentNullException(nameof(transactionSupervisor));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var hasTransactionAttribute = _requestHandler.GetType()
            .GetCustomAttributes(typeof(WithTransactionAttribute), false).Any();

        if (!hasTransactionAttribute)
        {
            return await next();
        }

        try
        {
            var requestName = request.GetType().FullName;
            _logger.Information(
                $"The transaction will be created by {requestName} ------ HANDLER WITH TRANSACTION ------- ");

            await _transactionSupervisor.GetOpenOrCreateTransaction();

            var response = await next();

            var result = _transactionSupervisor.Commit();

            if (result)
                _logger.Information(
                    $"Committed transaction {requestName} ------ COMMITTED TRANSACTION IN HANDLER ------- ");

            return response;
        }
        catch (Exception ex)
        {
            _transactionSupervisor.Rollback();

            var message = new
            {
                Message = "ERROR Handling transaction.",
                DataException = ex,
                Request = request
            };

            _logger.Error(message.Serialize());

            throw;
        }
        finally
        {
            _transactionSupervisor.Dispose();
        }
    }
}