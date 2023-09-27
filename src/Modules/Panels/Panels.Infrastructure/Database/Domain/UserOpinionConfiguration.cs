using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Users.Entities;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Infrastructure.Database.Domain;

public class UserOpinionConfiguration : IEntityTypeConfiguration<Opinion>
{
    public void Configure(EntityTypeBuilder<Opinion> builder)
    {
        builder.ToTable("UserOpinions", "panels");
        builder.HasKey(_ => _.Id);

        builder.Ignore(_ => _.Version);
        builder.Ignore(_ => _.Events);

        builder.Property(_ => _.CreatorId).HasColumnName("CreatorId").IsRequired();
        builder.Property(_ => _.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(_ => _.CreatorName).HasColumnName("CreatorName").IsRequired();
        builder.Property(_ => _.Created).HasColumnName("Created").IsRequired();
        builder.Property(_ => _.LastModified).HasColumnName("LastModified").IsRequired();

        builder.Property(_ => _.Content)
          .HasColumnName("Content")
          .HasConversion(x => x.Value, x => new Content(x)).IsRequired(false);

        builder.Property(_ => _.RatingTechnicalSkills)
          .HasColumnName("RatingTechnicalSkills")
          .HasConversion(x => x.Value, x => new Rating(x)).IsRequired(false);

        builder.Property(_ => _.RatingSoftSkills)
          .HasColumnName("RatingSoftSkills")
          .HasConversion(x => x.Value, x => new Rating(x)).IsRequired(false);
    }
}