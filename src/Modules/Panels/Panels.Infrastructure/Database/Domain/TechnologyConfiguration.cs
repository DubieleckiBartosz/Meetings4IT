using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Panels.Domain.Meetings.Categories;
using Panels.Domain.Users.Technologies;

namespace Panels.Infrastructure.Database.Domain;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.ToTable("Technologies", "panels");

        builder.HasKey(_ => _.Index);
        builder.Property(_ => _.Index)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

        builder
            .Property(_ => _.Value)
            .HasColumnName("Value").IsRequired()
        .IsRequired();

        builder.HasData(
            new MeetingCategory(1, ".NET"),
            new MeetingCategory(2, "JAVA"),
            new MeetingCategory(3, "PYTHON"),
            new MeetingCategory(4, "C++"),
            new MeetingCategory(5, "R"),
            new MeetingCategory(6, "SQL"),
            new MeetingCategory(7, "PostgreSQL"),
            new MeetingCategory(8, "RUBY"),
            new MeetingCategory(9, "DEVOPS"),
            new MeetingCategory(10, "MONGODB"),
            new MeetingCategory(11, "DOCKER"));
    }
}