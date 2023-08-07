using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects; 

namespace Panels.Domain.Users.ValueObjects;

public class UserImage : ValueObject
{
    public int UserId { get; }
    public string Key { get; }

    private UserImage(int userId, string key)
    {
        UserId = userId;
        Key = key;
    }

    public static UserImage Create(int userId, string key)
    {
        return new UserImage(userId, key);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return UserId;
        yield return Key;
    }
}