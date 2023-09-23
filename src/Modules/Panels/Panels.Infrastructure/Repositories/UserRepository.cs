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

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _users.AddAsync(user, cancellationToken);
    }

    public async Task<bool> UserExistsAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _users.AnyAsync(_ => _.Identifier == userId, cancellationToken);
    }

    public async Task<User?> GetUserByEmailNTAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _users.AsNoTracking().FirstOrDefaultAsync(_ => _.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserByEmailWithDetailsNTAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _users.Include("_stack.Technology")
            .AsNoTracking().FirstOrDefaultAsync(_ => _.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserByIdentifierAsync(string identifier, CancellationToken cancellationToken = default)
    {
        return await _users.FirstOrDefaultAsync(_ => _.Identifier == identifier, cancellationToken);
    }

    public void Update(User user)
    {
        _users.Update(user);
    }
}