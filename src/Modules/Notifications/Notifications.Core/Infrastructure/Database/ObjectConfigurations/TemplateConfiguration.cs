using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Core.Domain.Templates;
using Notifications.Core.Infrastructure.Database.ObjectConfigurations.Extensions;

namespace Notifications.Core.Infrastructure.Database.ObjectConfigurations;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("Templates", "notifications");

        builder.HasKey(x => x.Id);
        builder.Property(_ => _.Body).IsRequired();

        builder.ConfigureDefaultDateProperties();
    }
}