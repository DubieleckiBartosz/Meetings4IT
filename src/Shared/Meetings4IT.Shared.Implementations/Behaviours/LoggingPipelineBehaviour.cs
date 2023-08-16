using MediatR.Pipeline;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Tools;
using Serilog;

namespace Meetings4IT.Shared.Implementations.Behaviours;

public class LoggingPipelineBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUser _currentUser;

    public LoggingPipelineBehaviour(ILogger logger, ICurrentUser currentUser)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userName = _currentUser.UserName ?? string.Empty;
        var userRoles = _currentUser.AvailableRoles();

        _logger.Information(new
        { 
            Request = request,
            UserName = userName,
            RequestName = requestName,
            UserRoles = userRoles
        }.Serialize());

        return Task.CompletedTask;
    }
}