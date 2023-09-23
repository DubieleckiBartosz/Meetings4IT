using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Users;

namespace Panels.Infrastructure.Database.Domain;

public class UserConfiguration : WatcherConfiguration, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "panels");
        builder.HasKey(_ => _.Id);
        builder.Ignore(x => x.Events);

        builder.Property(p => p.Email)
          .HasColumnName("Email")
          .HasConversion(x => x.Value, x => new Email(x))
          .IsRequired();

        builder.Property(p => p.Name)
          .HasColumnName("Name").IsRequired();

        builder.Property(p => p.Identifier)
          .HasColumnName("Identifier").IsRequired();

        builder.OwnsOne(_ => _.Image, b =>
        {
            b.WithOwner().HasForeignKey(_ => _.UserId);
            b.ToTable("UserImages", "panels");

            b.Property(_ => _.Key).HasColumnName("Key").IsRequired();

            this.ConfigureWatcher(b);
        });

        builder.Ignore(_ => _.TechStack);
    }
}