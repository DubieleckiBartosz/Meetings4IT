using MediatR.Pipeline;
using Serilog;

namespace Meetings4IT.Shared.Implementations.Behaviours;

public class LoggingPipelineBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingPipelineBehaviour(ILogger logger)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;
        _logger.Information($"TeamTitan Request: {name} - {request}");

        return Task.CompletedTask;
    }
}