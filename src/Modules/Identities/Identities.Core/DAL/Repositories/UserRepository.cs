using Identities.Core.Enums;
using Identities.Core.Interfaces.Repositories;
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

    public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> UserToRoleAsync(ApplicationUser user, Roles role)
    {
        return await _userManager.AddToRoleAsync(user, role.ToString());
    }

    public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
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

    public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }

    public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }

    public async Task<IdentityResult> ResetUserPasswordAsync(ApplicationUser user, string token, string password)
    {
        var resetPassResult = await _userManager.ResetPasswordAsync(user, token, password);
        return resetPassResult;
    }

    public async Task<IdentityResult> ConfirmUserAsync(ApplicationUser user, string token)
    {
        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result;
    }
}