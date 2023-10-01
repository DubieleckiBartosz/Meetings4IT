using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panels.Domain.Meetings.Categories;

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

        builder.HasData(
            new MeetingCategory(1, "Party"),
            new MeetingCategory(2, "Social"),
            new MeetingCategory(3, "Business"),
            new MeetingCategory(4, "SomeCoffee"),
            new MeetingCategory(5, "Mentoring"),
            new MeetingCategory(6, "Unknown"));
    }
}