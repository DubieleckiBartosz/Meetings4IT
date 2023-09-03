using Notifications.Core.Infrastructure.Database;

namespace Notifications.Core.Infrastructure.Repositories;

public class TemplateRepository
{
    private readonly NotificationContext _context;

    public TemplateRepository(NotificationContext context)
    {
        _context = context;
    }

}