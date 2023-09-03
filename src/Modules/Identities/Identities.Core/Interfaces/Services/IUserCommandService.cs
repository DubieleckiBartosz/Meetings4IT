using Identities.Core.Models.Parameters;
using Meetings4IT.Shared.Implementations.Wrappers;

namespace Identities.Core.Interfaces.Services;

public interface IUserCommandService
{
    Task<Response> RegisterUserAsync(RegisterUserParameters parameters);
    Task<Response> ResetPasswordUserAsync(ResetPasswordParameters parameters);
    Task<Response> UpdateDataUserAsync(UpdateUserParameters parameters);
    Task<Response> ForgotPasswordAsync(ForgotPasswordParameters parameters);
    Task<Response> ConfirmUserAsync(string tokenConfirmation, string email);
}