using Meetings4IT.Shared.Abstractions.Events;
using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Panels.Domain.Meetings.Events;

public record MeetingCreated(
    int MeetingId,
    UserInfo MeetingCreator,
    DateRange Date) : IDomainEvent
{
    public static MeetingCreated Create(int meetingId, UserInfo meetingCreator, DateRange date)
    {
        return new MeetingCreated(meetingId, meetingCreator, date);
    }
}