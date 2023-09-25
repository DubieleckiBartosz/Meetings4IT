using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Panels.Domain.Users.Exceptions;
using Panels.Domain.Users.Technologies;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Domain.Users;

public class User : Entity, IAggregateRoot
{
    private readonly List<UserTechnology> _stack;
    public List<UserTechnology> TechStack => _stack;
    public UserImage? Image { get; private set; }
    public string Identifier { get; }
    public string Name { get; }
    public Email Email { get; private set; }
    public City City { get; private set; }

    private User()
    {
        _stack = new();
    }

    private User(string identifier, string name, Email email, City city)
    {
        Identifier = identifier ?? throw new ArgumentNullException("User identifier cannot be null.");
        Name = name ?? throw new ArgumentNullException("Name cannot be null.");
        City = name ?? throw new ArgumentNullException("City cannot be null.");
        Email = email ?? throw new ArgumentNullException("Email cannot be null.");

        _stack = new();
        this.IncrementVersion();
        City = city;
    }

    public static User Create(
        string identifier,
        string name,
        Email email,
        City city)
    {
        return new User(identifier, name, email, city);
    }

    public void CompleteDetails(UserImage? image, List<Technology>? stack)
    {
        Image = image;

        if (stack != null && stack.Any())
        {
            foreach (var techItem in stack)
            {
                var userTech = new UserTechnology(techItem, this);

                _stack.Add(userTech);
            }
        }

        this.IncrementVersion();
    }

    public void AddNewTechnology(Technology technology)
    {
        var technologyAlreadyExists = _stack.FirstOrDefault(_ => _.Technology.Equals(technology));
        if (technologyAlreadyExists != null)
        {
            throw new TechnologyExistsException();
        }

        var userNewTech = new UserTechnology(technology, this);

        _stack.Add(userNewTech);
        this.IncrementVersion();
    }

    public void RemoveTechnology(string technology)
    {
        var tech = _stack.FirstOrDefault(_ => _.Technology.Value == technology);
        if (tech == null)
        {
            throw new TechnologyNotFound(technology);
        }

        _stack.Remove(tech);
        this.IncrementVersion();
    }

    public void SetProfileImage(UserImage image)
    {
        Image = image;
        this.IncrementVersion();
    }
}