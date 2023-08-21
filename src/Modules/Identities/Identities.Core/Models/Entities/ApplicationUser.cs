﻿using Microsoft.AspNetCore.Identity;

namespace Identities.Core.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}