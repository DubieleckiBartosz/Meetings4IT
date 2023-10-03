using Panels.Domain.Meetings.Categories;
using Panels.Domain.Users.Technologies;

namespace Panels.Infrastructure.Database.Domain.Seed;

public class SeedData
{
    public static List<MeetingCategory> MeetingCategories() => new List<MeetingCategory>()
    {
        new MeetingCategory(1, "Party"),
        new MeetingCategory(2, "Social"),
        new MeetingCategory(3, "Business"),
        new MeetingCategory(4, "SomeCoffee"),
        new MeetingCategory(5, "Mentoring"),
        new MeetingCategory(6, "Unknown")
    };

    public static List<Technology> Technologies() => new List<Technology>()
    {
            new Technology(1, ".NET"),
            new Technology(2, "JAVA"),
            new Technology(3, "PYTHON"),
            new Technology(4, "C++"),
            new Technology(5, "R"),
            new Technology(6, "SQL"),
            new Technology(7, "PostgreSQL"),
            new Technology(8, "RUBY"),
            new Technology(9, "DEVOPS"),
            new Technology(10, "MONGODB"),
            new Technology(11, "DOCKER")
    };
}