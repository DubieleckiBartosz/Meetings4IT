using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Users.Technologies;
using Panels.Infrastructure.Database.Domain.Seed;

namespace Panels.Infrastructure.Database.Domain;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.ToTable("Technologies", "panels");

        builder.HasKey(_ => _.Index);
        builder.Property(_ => _.Index)
               .HasColumnType("int")
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

        builder
            .Property(_ => _.Value)
            .HasColumnType("varchar(25)")
            .HasColumnName("Value").IsRequired()
        .IsRequired();

        builder.HasData(SeedData.Technologies());
    }
}