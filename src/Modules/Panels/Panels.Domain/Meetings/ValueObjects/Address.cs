using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.ValueObjects;

public class Address : ValueObject
{
    public string City { get; }
    public string Street { get; }
    public string NumberStreet { get; }

    private Address(string city, string street, string numberStreet)
    {
        this.City = city;
        this.Street = street;
        this.NumberStreet = numberStreet;
    }

    public static Address Create(string city, string street, string numberStreet)
    {
        return new Address(city, street, numberStreet);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.City;
        yield return this.Street;
        yield return this.NumberStreet;
    }
}