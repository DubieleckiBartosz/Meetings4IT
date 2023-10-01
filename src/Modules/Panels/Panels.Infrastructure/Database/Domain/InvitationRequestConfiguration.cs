using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Meetings.Entities;
using Panels.Infrastructure.Database.Domain.Converters;

namespace Panels.Infrastructure.Database.Domain;

public class InvitationRequestConfiguration : IEntityTypeConfiguration<InvitationRequest>
{
    public void Configure(EntityTypeBuilder<InvitationRequest> builder)
    {
        builder.ToTable("InvitationRequests", "panels");
        builder.HasKey(_ => _.Id);

        builder.Property(p => p.Status)
          .HasColumnName("Status")
          .HasColumnType("tinyint")
          .HasConversion<RequestStatusConverter>()
          .IsRequired();

        builder.Property(_ => _.Created).HasColumnName("Created").IsRequired();
        builder.Property(_ => _.LastModified).HasColumnName("LastModified").IsRequired();
        builder.Property(_ => _.ReasonRejection).HasColumnName("ReasonRejection").IsRequired(false);

        builder.OwnsOne<UserInfo>("RequestCreator", _ =>
        {
            _.Property(p => p.Name)
                .HasColumnName("CreatorName")
                .HasColumnType("varchar(50)").IsRequired();

            _.Property(p => p.Identifier)
                .HasColumnName("CreatorId").IsRequired();
        });

        builder.Ignore(_ => _.Version);
        builder.Ignore(_ => _.Events);
    }
}