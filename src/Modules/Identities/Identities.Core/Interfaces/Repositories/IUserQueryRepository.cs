using Identities.Core.Models.Entities;

namespace Identities.Core.Interfaces.Repositories;

public interface IUserQueryRepository
{
    Task<ApplicationUser?> GetUserByNameAsync(string userName);
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task<string[]> GetUserRolesByUserAsync(ApplicationUser user);
    Task<ApplicationUser?> GetUserByIdAsync(string userId);
    Task<bool> UserIsBlockedAsync(ApplicationUser user, string password);
    Task<bool> UserIsStillAllowedToSignInAsync(ApplicationUser user);
}