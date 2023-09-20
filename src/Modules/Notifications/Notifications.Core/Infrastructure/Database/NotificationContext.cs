using Microsoft.EntityFrameworkCore;
using Notifications.Core.Domain.Alerts;
using Notifications.Core.Domain.Templates;

namespace Notifications.Core.Infrastructure.Database;

public class NotificationContext : DbContext
{
    public DbSet<Alert> Alerts { get; set; }
    public DbSet<AlertDetails> AlertDetails { get; set; }
    public DbSet<Template> Templates { get; set; }

    public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("notifications");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(modelBuilder);
    }
}