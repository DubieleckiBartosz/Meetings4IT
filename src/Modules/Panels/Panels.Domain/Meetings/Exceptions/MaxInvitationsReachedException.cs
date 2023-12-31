﻿using Meetings4IT.Shared.Abstractions.Exceptions;

namespace Panels.Domain.Meetings.Exceptions;

public class MaxInvitationsReachedException : BaseException
{
    public MaxInvitationsReachedException(int maxInvitations, int acceptedInvitations, int pendingInvitations) : base(
        $"The maximum number of invitations is {maxInvitations}. There are {pendingInvitations} invitations pending and {acceptedInvitations} have been accepted.")
    {
    }
}