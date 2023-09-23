using Microsoft.EntityFrameworkCore;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.Users.Technologies;
using Panels.Infrastructure.Database;

namespace Panels.Infrastructure.Repositories;

public class TechnologyRepository : ITechnologyRepository

{
    private readonly DbSet<Technology> _technologies;

    public TechnologyRepository(PanelContext context)
    {
        _technologies = context.Technologies;
    }

    public async Task<List<Technology>> GetAllTechnologiesNTAsync(CancellationToken cancellationToken = default)
    {
        return await _technologies.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Technology?> GetTechnologyByNameNTAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _technologies.AsNoTracking().FirstOrDefaultAsync(_ => _.Value.ToLower() == name.ToLower(), cancellationToken);
    }
}