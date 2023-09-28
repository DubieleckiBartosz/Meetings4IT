using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters;

public class AddNewUserOpinionParameters
{
    public int? RatingSoftSkills { get; init; }
    public int UserId { get; init; }
    public int? RatingTechSkills { get; init; }
    public string? Content { get; init; }

    public AddNewUserOpinionParameters()
    {
    }

    [JsonConstructor]
    public AddNewUserOpinionParameters(
        int? ratingSoftSkills,
        int userId,
        string? comtent,
        int? ratingTechSkills)
        => (RatingSoftSkills, UserId, Content, RatingTechSkills) = (ratingSoftSkills, userId, comtent, ratingTechSkills);
}