using Notifications.Core.Domain.Alerts;
using Notifications.Core.Domain.Alerts.ValueTypes;

namespace Notifications.Core.Interfaces.Repositories;

public interface IAlertRepository
{
    Task AddAlertAsync(Alert alert, CancellationToken cancellationToken = default);

    void Update(Alert alert);

    Task SaveAsync(CancellationToken cancellationToken = default);

    Task<Alert?> GetUnreadedAlertByIdAsync(int alertId, CancellationToken cancellationToken = default);

    Task<List<Alert>?> GetUnreadedAlertsByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    Task<AlertDetails?> GetAlertDetailsByTypeAsync(AlertType alertType, CancellationToken cancellationToken = default);
}