using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Core.Domain.Alerts;
using Notifications.Core.Infrastructure.Database.ObjectConfigurations.Extensions;

namespace Notifications.Core.Infrastructure.Database.ObjectConfigurations;

public class AlertMessageConfiguration : IEntityTypeConfiguration<AlertDetails>
{
    public void Configure(EntityTypeBuilder<AlertDetails> builder)
    {
        builder.ToTable("AlertDetails", "notifications");

        builder.HasKey(_ => _.AlertDetailsId);

        builder.Property(_ => _.AlertDetailsId).HasColumnName("AlertDetailsId").IsRequired();
        builder.Property(_ => _.Title).HasColumnName("Title").IsRequired();
        builder.Property(_ => _.MessageTemplate).HasColumnName("MessageTemplate").IsRequired();

        builder.ConfigureDefaultDateProperties(modified: false);
    }
}