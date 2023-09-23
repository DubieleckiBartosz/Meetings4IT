using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Users.Technologies;

namespace Panels.Infrastructure.Database.Domain;

public class UserTechnologyConfiguration : IEntityTypeConfiguration<UserTechnology>
{
    public void Configure(EntityTypeBuilder<UserTechnology> builder)
    {
        builder.HasKey(_ => new { _.UserId, _.TechnologyIndex });

        builder.HasOne(_ => _.User)
            .WithMany("_stack")
            .HasForeignKey(_ => _.UserId);

        builder.HasOne(_ => _.Technology)
            .WithMany()
            .HasForeignKey(_ => _.TechnologyIndex);
    }
}