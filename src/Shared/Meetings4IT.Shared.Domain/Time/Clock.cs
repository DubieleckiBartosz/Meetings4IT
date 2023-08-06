using Meetings4IT.Shared.Domain.Kernel.ValueObjects;

namespace Meetings4IT.Shared.Domain.Time;

public class Clock
{
    public static DateTime CurrentDate() => DateTime.UtcNow;
    public static Date CurrentDateObject() => new Date(CurrentDate());
}