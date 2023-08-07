using Meetings4IT.Shared.Domain.Kernel;
using Panels.Domain.Users.Exceptions;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Domain.Users;

public class User : Entity, IAggregateRoot
{
    private readonly HashSet<Technology> _stack = new(); 
    public List<Technology> TechStack => _stack.ToList();
    public UserImage Image { get; private set; }

    public User(UserImage image, List<Technology>? stack)
    {
        Image = image;
        if (stack != null && stack.Any())
        {
            foreach (var techItem in stack)
            {
                _stack.Add(techItem);
            }
        }

        this.IncrementVersion();
    }

    public void AddNewTechnology(Technology technology)
    {
        var technologyAlreadyExists = _stack.FirstOrDefault(_ => _.Value == technology);
        if (technologyAlreadyExists != null)
        {
            throw new TechnologyExistsException();
        }

        _stack.Add(technology);
        this.IncrementVersion();
    }

    public void RemoveTechnology(string technology)
    {
        var removedPositive = _stack.RemoveWhere(_ => _.Value == technology);
        if (removedPositive == 0)
        {
            throw new TechnologyNotFound(technology);
        }
        this.IncrementVersion();
    }

    public void SetProfileImage(UserImage image)
    {
        Image = image;
        this.IncrementVersion();
    }
}