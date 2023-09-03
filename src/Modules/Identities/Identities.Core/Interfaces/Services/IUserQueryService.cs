using Identities.Core.Models.Entities;

namespace Identities.Core.Interfaces.Services;

public interface IUserQueryService
{
    Task<ApplicationUser?> GetUserByNameAsync(string userName);

    Task<string[]> GetUserRolesByUserAsync(ApplicationUser user);

    Task<ApplicationUser?> GetUserByIdAsync(string userId);

    Task<bool> UserIsBlockedAsync(ApplicationUser user, string password);

    Task<bool> UserIsStillAllowedToSignInAsync(ApplicationUser user);
}