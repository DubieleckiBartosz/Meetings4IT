using Identities.Core.Interfaces;
using Identities.Core.Models.DataTransferObjects;
using Identities.Core.Models.Entities;
using Meetings4IT.Shared.Implementations.AutoMapper;

namespace Identities.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<UserDto?> GetUserByNameAsync(string userName)
    {
        var applicationUser = await _userRepository.GetUserByNameAsync(userName);
        return applicationUser?.Map<ApplicationUser, UserDto>();
    }

    public async Task<string[]> GetUserRolesByUserAsync(UserDto user)
    {
        var mapResult = user.Map<UserDto, ApplicationUser>();
        var userRoles = await _userRepository.GetUserRolesByUserAsync(mapResult);

        return userRoles;
    }

    public async Task<UserDto?> GetUserByIdAsync(string userId)
    {
        var applicationUser = await _userRepository.GetUserByIdAsync(userId);
        return applicationUser?.Map<ApplicationUser, UserDto>();
    }

    public async Task<bool> UserIsBlockedAsync(UserDto user, string password)
    {
        var mapResult = user.Map<UserDto, ApplicationUser>();
        return await _userRepository.UserIsBlockedAsync(mapResult, password);
    }

    public async Task<bool> UserIsStillAllowedToSignInAsync(UserDto user)
    {
        var mapResult = user.Map<UserDto, ApplicationUser>();
        return await _userRepository.UserIsStillAllowedToSignInAsync(mapResult);
    }
}