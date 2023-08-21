using OpenIddict.EntityFrameworkCore.Models;

namespace Identities.Core.Models.Entities.OpenIdDictCustomEntities;

public class CustomTokenEntity : OpenIddictEntityFrameworkCoreToken<string, CustomApplicationEntity, CustomAuthorizationEntity>
{
}