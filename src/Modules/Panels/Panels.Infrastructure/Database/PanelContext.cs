using Meetings4IT.Shared.Abstractions.Events;
using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Time;
using Meetings4IT.Shared.Implementations.EntityFramework.Extensions;
using Meetings4IT.Shared.Implementations.Outbox;
using Meetings4IT.Shared.Implementations.Outbox.DbConfig;
using Microsoft.EntityFrameworkCore;
using Panels.Domain.Meetings;
using Panels.Domain.Meetings.Categories;
using Panels.Domain.Meetings.Entities;
using Panels.Domain.ScheduledMeetings;
using Panels.Domain.Users;
using Panels.Domain.Users.Technologies;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Infrastructure.Database;

public class PanelContext : DbContext, IUnitOfWork
{
    private readonly IDomainDecorator _decorator;
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<MeetingCategory> MeetingCategories { get; set; }
    public DbSet<ScheduledMeeting> ScheduledMeetings { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public PanelContext(DbContextOptions<PanelContext> options) : base(options)
    { }

    public PanelContext(IDomainDecorator decorator, DbContextOptions<PanelContext> options) : base(options)
    {
        _decorator = decorator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("panels");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PanelContext).Assembly);
        modelBuilder.ApplyConfiguration(new OutboxMessageTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var watcherTypes = new Type[] { typeof(Invitation), typeof(UserImage) };
        var entries = ChangeTracker
            .Entries()
            .Where(e => watcherTypes.Contains(e.Entity.GetType()) && e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    var currentDate = Clock.CurrentDate();
                    entityEntry.Property("Created").CurrentValue = currentDate;
                    entityEntry.Property("LastModified").CurrentValue = currentDate;
                    break;

                case EntityState.Modified:
                    entityEntry.Property("LastModified").CurrentValue = Clock.CurrentDate();
                    break;

                case EntityState.Deleted:
                    entityEntry.State = EntityState.Modified;
                    entityEntry.Property("DeletedAt").CurrentValue = Clock.CurrentDate();
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        await this._decorator.DispatchDomainEventsAsync(this);

        return await SaveChangesAsync(cancellationToken);
    }
}