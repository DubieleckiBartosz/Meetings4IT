using Panels.Domain.DomainServices.DomainServiceInterfaces;
using Panels.Domain.Users;
using Panels.Domain.Users.Technologies;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Domain.DomainServices;

public class UserDomainService : IUserDomainService
{
    public User Complete(
        User user,
        List<string>? userNewTechnologies,
        UserImage? userimage,
        List<Technology>? existingTechnologies)
    {
        var technologies = new List<Technology>();

        if (existingTechnologies != null && existingTechnologies.Any())
        {
            foreach (var techItem in userNewTechnologies!)
            {
                var result = existingTechnologies.FirstOrDefault(_ => _.Value == techItem.ToUpper());
                if (result == null)
                {
                    var newTechnology = new Technology(techItem.ToUpper());
                    technologies.Add(newTechnology);
                    continue;
                }

                technologies.Add(result);
            }
        }

        user.CompleteDetails(userimage, technologies);

        return user;
    }
}