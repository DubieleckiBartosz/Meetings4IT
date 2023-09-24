using Meetings4IT.Shared.Abstractions.Exceptions;
using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Notifications.Core.Interfaces.Repositories;
using Serilog;

namespace Notifications.Core.Handlers.Alerts.Commands.ReadAlerts;

public class ReadAlertsHandler : ICommandHandler<ReadAlertsCommand, Response>
{
    private readonly IAlertRepository _alertRepository;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger _logger;

    public ReadAlertsHandler(IAlertRepository alertRepository, ICurrentUser currentUser, ILogger logger)
    {
        _alertRepository = alertRepository;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<Response> Handle(ReadAlertsCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var idsCount = request.AlertIdentifiers.Count;

        if (idsCount > 1)
        {
            var alerts = await _alertRepository.GetUnreadedAlertsByUserIdAsync(userId, cancellationToken);

            if (alerts == null || !alerts.Any())
            {
                throw new NotFoundException($"No unread user {userId} alerts found.");
            }

            foreach (var alertItem in alerts)
            {
                if (request.AlertIdentifiers.Contains(alertItem.Id))
                {
                    alertItem.Read();
                    _alertRepository.Update(alertItem);
                    _logger.Information($"Alert {alertItem.Id} status is set to read.");
                }
            }

            await _alertRepository.SaveAsync(cancellationToken);
            return Response.Ok();
        }

        if (idsCount == 1)
        {
            var alertId = request.AlertIdentifiers[0];
            var alert = await _alertRepository.GetUnreadedAlertByIdAsync(alertId, cancellationToken);
            if (alert == null || alert.RecipientId != userId)
            {
                throw new NotFoundException($"User {userId} alert {alertId} not found.");
            }

            alert.Read();
            _alertRepository.Update(alert);
            await _alertRepository.SaveAsync(cancellationToken);

            return Response.Ok();
        }

        throw new BadRequestException("List of alert identifiers must be greater than 0");
    }
}