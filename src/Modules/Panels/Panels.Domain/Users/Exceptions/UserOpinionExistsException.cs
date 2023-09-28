using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Users.Exceptions;

public class UserOpinionExistsException : BaseException
{
    public UserOpinionExistsException(int userId, string creatorOpinionId, int opinionId)
        : base($"Every user can add only one opinion to another user. [UserId {userId}, Creator {creatorOpinionId}, OpinionId {opinionId}]")
    {
    }
}