using Identities.Core.Enums;
using Identities.Core.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identities.Core.DAL.Seed;

public class DataSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task SeedRolesAsync()
    {
        string[] roles = { Roles.Admin.ToString(), Roles.User.ToString() };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task SeedAdminAsync()
    {
        var userAdmin = new ApplicationUser()
        {
            UserName = "admin_user_name",
            Email = "admin.meetings@dev.com",
            FirstName = "admin_dev",
            LastName = "admin_meetings_dev",
            EmailConfirmed = true
        };

        await SaveUser(userAdmin, "PasswordAdmin$123", Roles.Admin.ToString());

        var user = new ApplicationUser()
        {
            UserName = "user_name",
            Email = "user.meetings@dev.com",
            FirstName = "user_dev",
            LastName = "user_meetings_dev",
            EmailConfirmed = true
        };

        await SaveUser(user, "PasswordUser$123", Roles.User.ToString());
    }

    private async Task SaveUser(ApplicationUser user, string password, string role)
    {
        var result = await _userManager.FindByEmailAsync(user.Email);
        if (result == null)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, role);

            await _context.SaveChangesAsync();
        }
    }
}