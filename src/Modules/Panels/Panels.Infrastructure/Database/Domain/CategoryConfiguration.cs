using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Meetings.Categories;
using Panels.Infrastructure.Database.Domain.Seed;

namespace Panels.Infrastructure.Database.Domain;

public class CategoryConfiguration : IEntityTypeConfiguration<MeetingCategory>
{
    public void Configure(EntityTypeBuilder<MeetingCategory> builder)
    {
        builder.ToTable("MeetingCategories", "panels");

        builder.HasKey(category => category.Index);
        builder.Property(category => category.Index)
               .ValueGeneratedOnAdd()
               .HasColumnType("tinyint")
               .UseIdentityColumn();

        builder
            .Property(category => category.Value)
            .HasColumnName("Value")
            .HasColumnType("varchar(30)").IsRequired();

        builder.HasData(SeedData.MeetingCategories());
    }
}