using Microsoft.EntityFrameworkCore;
using Notifications.Core.Domain.Templates;

namespace Notifications.Core.Infrastructure.Database;

public class NotificationContext : DbContext
{
    public DbSet<Template> Templates { get; set; }

    public NotificationContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("notifications");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(modelBuilder);
    }

    //public async Task<int> SaveChangesAsync()
    //{
    //    var modifiedEntities = ChangeTracker.Entries()
    //     .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

    //    var currentTime = Clock.CurrentDate();

    //    foreach (var entry in modifiedEntities)
    //    {
    //        if (entry.State == EntityState.Added)
    //        {
    //            entry.Property("Created").CurrentValue = currentTime;
    //        }

    //        entry.Property("Modified").CurrentValue = currentTime;
    //    }

    //    return await base.SaveChangesAsync();
    //}
}