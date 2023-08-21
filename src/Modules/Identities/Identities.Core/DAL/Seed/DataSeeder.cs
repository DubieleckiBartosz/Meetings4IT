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
        var adminDevEmail = "admin.meetings@dev.com";
        var resultAdmin = await _userManager.FindByEmailAsync(adminDevEmail);
        if (resultAdmin == null)
        {
            var user = new ApplicationUser()
            {
                UserName = "admin_user_name",
                Email = adminDevEmail,
                FirstName = "admin_dev",
                LastName = "admin_meetings_dev",
            };

            await _userManager.CreateAsync(user, "PasswordAdmin$123");
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

            await _context.SaveChangesAsync();
        }
    }
}