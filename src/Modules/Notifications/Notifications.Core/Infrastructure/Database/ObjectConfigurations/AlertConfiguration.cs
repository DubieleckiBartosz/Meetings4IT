using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Core.Domain.Alerts.Entities;
using Notifications.Core.Infrastructure.Database.ObjectConfigurations.Extensions;

namespace Notifications.Core.Infrastructure.Database.ObjectConfigurations;

public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        builder.ToTable("Alerts", "notifications");

        builder.HasKey(x => x.Id);

        builder.ConfigureDefaultDateProperties();
    }
}