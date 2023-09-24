using Microsoft.EntityFrameworkCore;
using Notifications.Core.Domain.Alerts;
using Notifications.Core.Domain.Alerts.ValueTypes;
using Notifications.Core.Infrastructure.Database;
using Notifications.Core.Interfaces.Repositories;

namespace Notifications.Core.Infrastructure.Repositories;

public class AlertRepository : IAlertRepository
{
    private readonly DbSet<Alert> _alerts;
    private readonly DbSet<AlertDetails> _messages;
    private readonly NotificationContext _context;

    public AlertRepository(NotificationContext context)
    {
        _context = context;
        _alerts = _context.Alerts;
        _messages = _context.AlertDetails;
    }

    public async Task AddAlertAsync(Alert alert, CancellationToken cancellationToken = default)
    {
        await _alerts.AddAsync(alert, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Update(Alert alert)
    {
        _alerts.Update(alert);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Alert>?> GetUnreadedAlertsByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = await _alerts
            .Where(_ => _.RecipientId == userId && _.IsRead == false)
            .OrderBy(_ => _.Created)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<AlertDetails?> GetAlertDetailsByTypeAsync(AlertType alertType, CancellationToken cancellationToken = default)
    {
        return await _messages.FirstOrDefaultAsync(_ => _.AlertDetailsId == alertType, cancellationToken);
    }

    public async Task<Alert?> GetUnreadedAlertByIdAsync(int alertId, CancellationToken cancellationToken = default)
    {
        return await _alerts.FirstOrDefaultAsync(_ => _.Id == alertId && _.IsRead == false, cancellationToken);
    }
}