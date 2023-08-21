using OpenIddict.EntityFrameworkCore.Models;

namespace Identities.Core.Models.Entities.OpenIdDictCustomEntities;

public class CustomApplicationEntity : OpenIddictEntityFrameworkCoreApplication<string, CustomAuthorizationEntity, CustomTokenEntity>
{
}