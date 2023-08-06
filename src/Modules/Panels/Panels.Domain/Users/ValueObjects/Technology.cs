namespace Panels.Domain.Users.ValueObjects;

public class Technology
{
    public string Value { get; }

    public Technology(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), "TechStack cannot be null or empty");
        }

        this.Value = value;
    }

    public static implicit operator Technology(string value) => new(value);
    public static implicit operator string(Technology technology) => technology.Value;
}