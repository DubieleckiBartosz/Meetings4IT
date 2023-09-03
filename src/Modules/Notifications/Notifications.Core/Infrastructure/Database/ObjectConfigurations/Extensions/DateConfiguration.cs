using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notifications.Core.Infrastructure.Database.ObjectConfigurations.Extensions;

public static class DateConfiguration
{
    public static EntityTypeBuilder<TEntity> ConfigureDefaultDateProperties<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        builder.Property<DateTime>("Created")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property<DateTime>("Modified")
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();

        return builder;
    }
}