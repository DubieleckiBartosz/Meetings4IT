using Meetings4IT.Shared.Implementations.Dapper;
using Meetings4IT.Shared.Implementations.Options;
using Microsoft.Extensions.Options;
using Panels.Application.Contracts.ReadRepositories;
using Serilog;

namespace Panels.Infrastructure.Repositories.ReadRepositories;

public class UserReadRepository : BaseRepository, IUserReadRepository
{
    public UserReadRepository(IOptions<DapperOptions> options, ILogger logger) : base(options!.Value.DefaultConnection, logger)
    {
    }
}