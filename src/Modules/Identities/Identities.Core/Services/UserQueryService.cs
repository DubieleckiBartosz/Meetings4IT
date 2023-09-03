using Identities.Core.Interfaces.Repositories;
using Identities.Core.Interfaces.Services;
using Identities.Core.Models.Entities;

namespace Identities.Core.Services;

//We cannot use maps to objects because then we will lose the reference
public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _userRepository;

    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
    {
        var applicationUser = await _userRepository.GetUserByNameAsync(userName);
        return applicationUser;
    }

    public async Task<string[]> GetUserRolesByUserAsync(ApplicationUser user)
    {
        var userRoles = await _userRepository.GetUserRolesByUserAsync(user);

        return userRoles;
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        var applicationUser = await _userRepository.GetUserByIdAsync(userId);
        return applicationUser;
    }

    public async Task<bool> UserIsBlockedAsync(ApplicationUser user, string password)
    {
        return await _userRepository.UserIsBlockedAsync(user, password);
    }

    public async Task<bool> UserIsStillAllowedToSignInAsync(ApplicationUser user)
    {
        return await _userRepository.UserIsStillAllowedToSignInAsync(user);
    }
}