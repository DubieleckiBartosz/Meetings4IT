using Meetings4IT.Shared.Abstractions.Kernel;
using Microsoft.EntityFrameworkCore;
using Panels.Application.Contracts.Repositories;
using Panels.Domain.Users;
using Panels.Infrastructure.Database;

namespace Panels.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly PanelContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public UserRepository(PanelContext context)
    {
        _users = context.Users;
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _users.FirstOrDefaultAsync(_ => _.Email == email);
    }
}