using System.Net;
using Meetings4IT.Shared.Abstractions.Exceptions; 

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationNotFoundException : Meetings4IT.Shared.Abstractions.Exceptions.BaseException
{
    public InvitationNotFoundException(string email, int meetingId) : base($"No invite found for {email} in {meetingId} meeting", HttpStatusCode.NotFound)
    {
    }
}