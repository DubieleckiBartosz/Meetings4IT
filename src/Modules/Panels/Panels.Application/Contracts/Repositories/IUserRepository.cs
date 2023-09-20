using Panels.Domain.Users;

namespace Panels.Application.Contracts.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
}