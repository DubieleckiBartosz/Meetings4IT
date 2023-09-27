using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Users.Exceptions;

public class OpinionAllRatingPropertiesException : BaseException
{
    public OpinionAllRatingPropertiesException(int userId, string creatorId) : base($"When creating a user opinion, all rating properties cannot be null. [User {userId}, Creator {creatorId}]")
    {
    }
}