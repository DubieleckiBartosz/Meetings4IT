﻿using System.Net;
using Meetings4IT.Shared.Abstractions.Exceptions; 

namespace Panels.Domain.Meetings.Exceptions.InvitationExceptions;

public class InvitationNotFoundException : BusinessException
{
    public InvitationNotFoundException(string email, int meetingId) : base($"No invite found for {email} in {meetingId} meeting", HttpStatusCode.NotFound)
    {
    }
}