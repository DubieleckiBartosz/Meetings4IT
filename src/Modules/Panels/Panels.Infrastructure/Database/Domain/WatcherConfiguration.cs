using Meetings4IT.Shared.Abstractions.Kernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Panels.Infrastructure.Database.Domain;

public class WatcherConfiguration
{
    protected void ConfigureWatcher<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : Entity
    {
        builder.Property<DateTime>("Created").HasColumnName("Created").IsRequired();
        builder.Property<DateTime>("LastModified").HasColumnName("LastModified").IsRequired();
        builder.Property<DateTime?>("DeletedAt").HasColumnName("DeletedAt").IsRequired(false);
    }

    protected void ConfigureWatcher<TOwnerEntity, TDependentEntity>(OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> builder)
        where TOwnerEntity : class where TDependentEntity : class
    {
        builder.Property<DateTime>("Created").HasColumnName("Created").IsRequired();
        builder.Property<DateTime>("LastModified").HasColumnName("LastModified").IsRequired();
        builder.Property<DateTime?>("DeletedAt").HasColumnName("DeletedAt").IsRequired(false);
    }
}