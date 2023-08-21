using OpenIddict.EntityFrameworkCore.Models;

namespace Identities.Core.Models.Entities.OpenIdDictCustomEntities;

public class CustomAuthorizationEntity : OpenIddictEntityFrameworkCoreAuthorization<string, CustomApplicationEntity, CustomTokenEntity>
{
    public string UserId { get; set; } = default!;
}