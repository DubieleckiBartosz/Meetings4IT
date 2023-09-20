using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Core.Domain.Alerts;
using Notifications.Core.Infrastructure.Database.ObjectConfigurations.Extensions;

namespace Notifications.Core.Infrastructure.Database.ObjectConfigurations;

public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        builder.ToTable("Alerts", "notifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id");

        builder.Property(x => x.RecipientId).HasColumnName("RecipientId").IsRequired();
        builder.Property(x => x.Type).HasColumnName("Type").IsRequired();
        builder.Property(x => x.IsRead).HasColumnName("IsRead").IsRequired();

        builder.ConfigureDefaultDateProperties();
    }
}