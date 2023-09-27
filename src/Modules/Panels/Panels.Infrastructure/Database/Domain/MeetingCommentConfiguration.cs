using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Meetings.Entities;

namespace Panels.Infrastructure.Database.Domain;

public class MeetingCommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("MeetingComments", "panels");
        builder.HasKey(_ => _.Id);

        builder.Ignore(_ => _.Version);
        builder.Ignore(_ => _.Events);

        builder.Property(_ => _.CreatorId).HasColumnName("CreatorId").IsRequired();
        builder.Property(_ => _.MeetingId).HasColumnName("MeetingId").IsRequired();
        builder.Property(_ => _.CreatorName).HasColumnName("CreatorName").IsRequired();
        builder.Property(_ => _.Created).HasColumnName("Created").IsRequired();
        builder.Property(_ => _.Modified).HasColumnName("Modified").IsRequired();

        builder.Property(_ => _.Content)
          .HasColumnName("Content")
          .HasConversion(x => x.Value, x => new Content(x)).IsRequired();
    }
}