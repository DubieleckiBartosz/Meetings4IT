using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Meetings.Entities;
using Panels.Domain.Meetings.ValueObjects;
using Panels.Infrastructure.Database.Domain.Converters;

namespace Panels.Infrastructure.Database.Domain;

public class InvitationConfiguration : WatcherConfiguration, IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.ToTable("Invitations", "panels");

        builder.HasKey(builder => builder.Id);

        builder.Property(p => p.Email)
          .HasColumnName("Email")
          .HasColumnType("varchar(50)")
          .HasConversion(x => x.Value, x => new Email(x))
          .IsRequired();

        builder.Property(p => p.Status)
          .HasColumnName("Status")
          .HasColumnType("tinyint")
          .HasConversion<InvitationStatusConverter>()
          .IsRequired();

        builder.Property(p => p.Code)
          .HasColumnName("Code")
          .HasColumnType("varchar(20)")
          .HasConversion(x => x.Value, x => InvitationCode.Create(x))
          .IsRequired();

        builder.Property(p => p.RecipientName)
          .HasColumnName("RecipientName")
          .HasColumnType("varchar(50)")
          .HasConversion(x => x.Value, x => new NameInvitationRecipient(x))
          .IsRequired();

        builder.Property(p => p.ExpirationDate)
          .HasColumnName("ExpirationDate")
          .HasConversion(x => x.Value, x => new Date(x))
          .IsRequired();

        builder.Property(p => p.RecipientId)
          .HasColumnName("RecipientId")
          .IsRequired(false);

        this.ConfigureWatcher(builder);

        builder.Ignore(x => x.Events);
        builder.Ignore(x => x.Version);
    }
}