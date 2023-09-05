using Microsoft.EntityFrameworkCore;
using Notifications.Core.Domain.Templates;
using Notifications.Core.Domain.Templates.ValueTypes;
using Notifications.Core.Infrastructure.Database;
using Notifications.Core.Interfaces.Repositories;

namespace Notifications.Core.Infrastructure.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private readonly NotificationContext _context;

    public TemplateRepository(NotificationContext context)
    {
        _context = context;
    }

    public async Task<Template?> TemplateByTypeAsync(TemplateType type)
    {
        var result = await _context.Templates.FirstOrDefaultAsync(_ => _.Type == type);
        return result;
    }
}