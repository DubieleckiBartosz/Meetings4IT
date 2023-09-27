using Meetings4IT.Shared.Abstractions.Exceptions;
using System.Net;

namespace Panels.Domain.Users.Exceptions;

public class OpinionNotFoundException : BaseException
{
    public OpinionNotFoundException(int opinionId, int userId, string creatorOpinionId)
        : base($"Opinion not found. [opinionId {opinionId}, userId {userId}, creatorOpinionId {creatorOpinionId}]", HttpStatusCode.NotFound)
    {
    }
}