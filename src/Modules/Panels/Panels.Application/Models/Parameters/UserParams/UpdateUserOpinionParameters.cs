using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters.UserParams;

public class UpdateUserOpinionParameters
{
    public int? RatingSoftSkills { get; init; }
    public int UserId { get; init; }
    public int? RatingTechSkills { get; init; }
    public string? Content { get; init; }
    public int OpinionId { get; init; }

    public UpdateUserOpinionParameters()
    {
    }

    [JsonConstructor]
    public UpdateUserOpinionParameters(
        int? ratingSoftSkills,
        int userId,
        string? comtent,
        int? ratingTechSkills,
        int opinionId)
        => (
        RatingSoftSkills,
        UserId,
        Content,
        RatingTechSkills,
        OpinionId) = (ratingSoftSkills, userId, comtent, ratingTechSkills, opinionId);
}