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
               .UseIdentityColumn();

        builder
            .Property(category => category.Value)
            .HasColumnName("Value").IsRequired()
            .IsRequired();
    }
}