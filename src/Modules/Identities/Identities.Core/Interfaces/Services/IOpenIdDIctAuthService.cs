namespace Identities.Core.Interfaces.Services;

internal interface IOpenIdDIctAuthService
{
    Task DeleteAllAuthorizationsForUser(string userId, CancellationToken cancellationToken = default);
}