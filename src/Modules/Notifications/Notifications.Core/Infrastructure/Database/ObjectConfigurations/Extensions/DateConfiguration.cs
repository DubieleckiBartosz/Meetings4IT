using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notifications.Core.Infrastructure.Database.ObjectConfigurations.Extensions;

public static class DateConfiguration
{
    public static EntityTypeBuilder<TEntity> ConfigureDefaultDateProperties<TEntity>(
        this EntityTypeBuilder<TEntity> builder, bool created = true, bool modified = true) where TEntity : class
    {
        if (created)
        {
            builder.Property<DateTime>("Created")
                .HasDefaultValueSql("GETUTCDATE()");
        }

        if (modified)
        {
            builder.Property<DateTime>("Modified")
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAddOrUpdate();
        }

        return builder;
    }
}