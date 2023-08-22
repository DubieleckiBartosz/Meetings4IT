using Identities.Core.Interfaces;
using Identities.Core.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identities.Core.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }
    public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<string[]> GetUserRolesByUserAsync(ApplicationUser user)
    {
        return (await _userManager.GetRolesAsync(user)).ToArray();
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
       return await _userManager.FindByIdAsync(userId);
    }

    public async Task<bool> UserIsBlockedAsync(ApplicationUser user, string password)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
        return !result.Succeeded;
    }

    public async Task<bool> UserIsStillAllowedToSignInAsync(ApplicationUser user)
    {
        var result = await _signInManager.CanSignInAsync(user);
        return result;
    }

    public async Task<string> GetEmailConfirmationTokenAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }
}