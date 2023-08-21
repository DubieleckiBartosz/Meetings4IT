using Identities.Core.Models.DataTransferObjects; 

namespace Identities.Core.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserByNameAsync(string userName);
    Task<string[]> GetUserRolesByUserAsync(UserDto user);
    Task<UserDto?> GetUserByIdAsync(string userId);
    Task<bool> UserIsBlockedAsync(UserDto user, string password);
    Task<bool> UserIsStillAllowedToSignInAsync(UserDto user);
}