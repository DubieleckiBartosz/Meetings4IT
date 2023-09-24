using Meetings4IT.Shared.Implementations.Mediator;
using Meetings4IT.Shared.Implementations.Services;
using Meetings4IT.Shared.Implementations.Wrappers;
using Notifications.Core.Interfaces.Repositories;
using Notifications.Core.Models.Views;

namespace Notifications.Core.Handlers.Alerts.Queries;

public class UnreadedAlertsHandler : IQueryHandler<UnreadedAlertsQuery, Response<List<AlertViewModel>?>>
{
    private readonly IAlertRepository _alertRepository;
    private readonly ICurrentUser _currentUser;

    public UnreadedAlertsHandler(IAlertRepository alertRepository, ICurrentUser currentUser)
    {
        _alertRepository = alertRepository;
        _currentUser = currentUser;
    }

    public async Task<Response<List<AlertViewModel>?>> Handle(UnreadedAlertsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var alerts = await _alertRepository.GetUnreadedAlertsByUserIdAsync(userId, cancellationToken);

        var unreadedAlerts = alerts?.Select(_ => new AlertViewModel(_.Id, _.Type, _.Message, _.Created))?.ToList();

        return Response<List<AlertViewModel>?>.Ok(unreadedAlerts);
    }
}