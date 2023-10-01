﻿using Newtonsoft.Json;

namespace Panels.Application.Models.Parameters;

public class AddInvitationRequestParameters
{
    public int MeetingId { get; init; }

    public AddInvitationRequestParameters()
    { }

    [JsonConstructor]
    public AddInvitationRequestParameters(int meetingId) => (MeetingId) = (meetingId);
}