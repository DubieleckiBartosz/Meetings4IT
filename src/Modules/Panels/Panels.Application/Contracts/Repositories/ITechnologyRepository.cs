using Panels.Domain.Users.Technologies;

namespace Panels.Application.Contracts.Repositories;

public interface ITechnologyRepository
{
    Task<List<Technology>> GetAllTechnologiesNTAsync(CancellationToken cancellationToken = default);

    Task<Technology?> GetTechnologyByNameNTAsync(string name, CancellationToken cancellationToken = default);
}