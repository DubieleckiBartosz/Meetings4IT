using Meetings4IT.Shared.Abstractions.Kernel;

namespace Panels.Domain.Meetings.Statuses;

public class MeetingStatus : Enumeration
{
    public static MeetingStatus Active = new MeetingStatus(1, nameof(Active));
    public static MeetingStatus Cancelled = new MeetingStatus(2, nameof(Cancelled));
    public static MeetingStatus Completed = new MeetingStatus(3, nameof(Completed));

    protected MeetingStatus(int id, string name) : base(id, name)
    {
    }
}