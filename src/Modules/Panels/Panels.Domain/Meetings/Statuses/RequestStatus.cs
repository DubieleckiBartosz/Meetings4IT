﻿using Meetings4IT.Shared.Abstractions.Kernel;

namespace Panels.Domain.Meetings.Statuses;

public class RequestStatus : Enumeration
{
    public static RequestStatus Pending = new RequestStatus(1, nameof(Pending));
    public static RequestStatus Accepted = new RequestStatus(2, nameof(Accepted));
    public static RequestStatus Rejected = new RequestStatus(3, nameof(Rejected));

    protected RequestStatus(int id, string name) : base(id, name)
    {
    }
}