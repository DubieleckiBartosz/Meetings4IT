using Panels.Domain.Users.Technologies;
using Panels.Domain.Users.ValueObjects;
using Panels.Domain.Users;

namespace Panels.Domain.DomainServices.DomainServiceInterfaces;

public interface IUserDomainService
{
    User Complete(User user, List<string>? userNewTechnologies, UserImage? userimage, List<Technology>? existingTechnologies);
}