using System.Net;

namespace Meetings4IT.Shared.Abstractions.Exceptions;

public class NoUserAccessOperation : BaseException
{
    public NoUserAccessOperation() :
        base("You don't have access to this operation", HttpStatusCode.Forbidden)
    {
    }

    public NoUserAccessOperation(string message) : base(message, HttpStatusCode.Forbidden)
    {
    }
}