using Identities.Core.Enums;
using Identities.Core.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identities.Core.Interfaces.Repositories;

public interface IUserCommandRepository
{
    Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);

    Task<IdentityResult> UserToRoleAsync(ApplicationUser user, Roles role);

    Task<IdentityResult> ConfirmUserAsync(ApplicationUser user, string token);

    Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

    Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);

    Task<IdentityResult> ResetUserPasswordAsync(ApplicationUser user, string token, string password);
}