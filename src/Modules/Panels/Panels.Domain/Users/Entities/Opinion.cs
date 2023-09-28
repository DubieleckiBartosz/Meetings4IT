using Meetings4IT.Shared.Abstractions.Kernel;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;
using Panels.Domain.Users.Exceptions;
using Panels.Domain.Users.ValueObjects;

namespace Panels.Domain.Users.Entities;

/// <summary>
/// We actually split user opinions and meeting comments
/// into two separate tables, but that's for optimization purposes.
/// User opinions are more extensive and doing a join every time may lead to a longer wait for the response
/// </summary>
public class Opinion : Entity
{
    public int UserId { get; }

    /// <summary>
    /// CreatorId for quick search
    /// </summary>
    public string CreatorId { get; }

    public string CreatorName { get; }

    public Rating? RatingTechnicalSkills { get; private set; }
    public Rating? RatingSoftSkills { get; private set; }
    public Content? Content { get; private set; }
    public DateTime Created { get; }
    public DateTime LastModified { get; private set; }

    private Opinion(
        int userId,
        string creatorId,
        string creatorName,
        Rating? ratingTechnicalSkills,
        Rating? ratingSoftSkills,
        Content? content)
    {
        if (ratingSoftSkills == null && ratingTechnicalSkills == null && content == null)
        {
            throw new OpinionAllRatingPropertiesException(userId, creatorId);
        }

        UserId = userId;
        CreatorId = creatorId;
        CreatorName = creatorName;
        RatingTechnicalSkills = ratingTechnicalSkills;
        RatingSoftSkills = ratingSoftSkills;
        Content = content;
        Created = Clock.CurrentDate();
        LastModified = Clock.CurrentDate();
    }

    public static Opinion CreateNewOpinion(
        int userId,
        string creatorId,
        string creatorName,
        Rating? ratingTechnicalSkills,
        Rating? ratingSoftSkills,
        Content? content)
    {
        return new Opinion(userId, creatorId, creatorName, ratingTechnicalSkills, ratingSoftSkills, content);
    }

    public void Update(Rating? ratingTechnicalSkills, Rating? ratingSoftSkills, Content? content)
    {
        RatingTechnicalSkills = ratingTechnicalSkills;
        LastModified = Clock.CurrentDate();
    }
}