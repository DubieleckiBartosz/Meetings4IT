using Meetings4IT.Shared.Abstractions.Kernel;
using Panels.Domain.Users;

namespace Panels.Application.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);

    void Update(User user);

    Task<bool> UserExistsAsync(string userId, CancellationToken cancellationToken = default);

    Task<User?> GetUserByEmailNTAsync(string email, CancellationToken cancellationToken = default);

    Task<User?> GetUserByEmailWithDetailsNTAsync(string email, CancellationToken cancellationToken = default);

    Task<List<User>?> GetUsersByEmailsNTAsync(List<string> emails, CancellationToken cancellationToken = default);

    Task<User?> GetUserByIdentifierNTAsync(string identifier, CancellationToken cancellationToken = default);

    Task<User?> GetUserByNameNTAsync(string name, CancellationToken cancellationToken = default);

    Task<User?> GetUserByIdentifierAsync(string identifier, CancellationToken cancellationToken = default);

    Task<User?> GetUserWithOpinionsById(int userId, CancellationToken cancellationToken = default);
}