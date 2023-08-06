using Meetings4IT.Shared.Domain.Kernel;

namespace Panels.Domain.Meetings.Statuses;

public class MeetingStatus : Enumeration
{
    public static MeetingStatus Pending = new MeetingStatus(1, nameof(Pending));
    public static MeetingStatus Suspended = new MeetingStatus(2, nameof(Suspended));
    public static MeetingStatus Cancelled = new MeetingStatus(3, nameof(Cancelled));
    public static MeetingStatus Done = new MeetingStatus(4, nameof(Done));

    protected MeetingStatus(int id, string name) : base(id, name)
    {
    }
}