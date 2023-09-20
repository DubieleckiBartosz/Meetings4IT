using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Panels.Domain.Users.Exceptions;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Domain.Users;

public class User : Entity, IAggregateRoot
{
    private readonly HashSet<Technology> _stack = new();
    public List<Technology> TechStack => _stack.ToList();
    public UserImage Image { get; private set; }
    public string Identifier { get; }
    public string Name { get; private set; }
    public Email Email { get; private set; }

    private User(string identifier, UserImage image, string name, Email email, List<Technology>? stack)
    {
        Identifier = identifier ?? throw new ArgumentNullException("User identifier cannot be null.");
        Name = name ?? throw new ArgumentNullException("Name cannot be null.");
        Email = email ?? throw new ArgumentNullException("Email cannot be null.");
        Image = image;
        if (stack != null && stack.Any())
        {
            foreach (var techItem in stack)
            {
                _stack.Add(techItem);
            }
        }

        this.IncrementVersion();
        Name = name;
        Email = email;
    }

    public static User Create(
        string identifier,
        UserImage image,
        string name,
        Email email,
        List<Technology>? stack)
    {
        return new User(identifier, image, name, email, stack);
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