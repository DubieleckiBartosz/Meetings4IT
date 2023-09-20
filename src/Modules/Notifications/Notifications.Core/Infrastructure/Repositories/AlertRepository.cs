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

    public async Task AddAlertAsync(Alert alert, CancellationToken cancellationToken)
    {
        await _alerts.AddAsync(alert, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<AlertDetails?> GetAlertDetailsByTypeAsync(AlertType alertType, CancellationToken cancellationToken)
    {
        return await _messages.FirstOrDefaultAsync(_ => _.AlertDetailsId == alertType, cancellationToken);
    }
}