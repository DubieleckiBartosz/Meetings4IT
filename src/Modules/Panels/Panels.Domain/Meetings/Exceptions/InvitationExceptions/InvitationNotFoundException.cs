using Meetings4IT.Shared.Abstractions.Exceptions;
using System.Net;

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationNotFoundException : BaseException
{
    public InvitationNotFoundException(string email, int meetingId) : base($"No invite found for {email} in {meetingId} meeting", HttpStatusCode.NotFound)
    {
    }
}