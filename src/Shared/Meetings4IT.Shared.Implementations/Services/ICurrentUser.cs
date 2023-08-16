namespace Meetings4IT.Shared.Implementations.Services;

public interface ICurrentUser
{
    bool IsAdmin { get; }
    bool IsInRole(string roleName);
    bool IsInRoles(string[] roles);
    List<string>? AvailableRoles();
    int UserId { get; } 
    string UserName { get; }
    string Email { get; }
}