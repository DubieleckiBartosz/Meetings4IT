using Newtonsoft.Json;

namespace Identities.Core.Models.Parameters;

public class RegisterUserParameters
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string City { get; init; }

    public RegisterUserParameters()
    {
    }

    [JsonConstructor]
    public RegisterUserParameters(
        string userName,
        string email,
        string phoneNumber,
        string password,
        string confirmPassword,
        string firstName,
        string lastName,
        string city)
    {
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        ConfirmPassword = confirmPassword;
        FirstName = firstName;
        LastName = lastName;
        City = city;
    }
}