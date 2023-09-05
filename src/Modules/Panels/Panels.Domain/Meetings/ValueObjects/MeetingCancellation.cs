using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;
using Meetings4IT.Shared.Abstractions.Time;

namespace Panels.Domain.Meetings.ValueObjects;

public class MeetingCancellation : ValueObject
{
    public string? Reason { get; }
    public Date CancellationDate { get; }

    private MeetingCancellation(string? reason)
    {
        Reason = reason;
        CancellationDate = Clock.CurrentDate();
    }

    public static MeetingCancellation CreateCancellation(string? reason)
    {
        return new MeetingCancellation(reason);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Reason;
        yield return CancellationDate;
    }
}