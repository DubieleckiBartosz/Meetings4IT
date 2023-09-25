using Microsoft.AspNetCore.Identity;

namespace Identities.Core.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
}