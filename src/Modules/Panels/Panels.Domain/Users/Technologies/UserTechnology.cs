namespace Panels.Domain.Users.Technologies;

public class UserTechnology
{
    public int TechnologyIndex { get; private set; }
    public Technology Technology { get; private set; }

    public int UserId { get; private set; }
    public User User { get; private set; }

    private UserTechnology()
    { }

    public UserTechnology(Technology technology, User user)
    {
        Technology = technology;
        User = user;
    }
}