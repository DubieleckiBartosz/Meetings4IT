using Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

namespace Meetings4IT.Shared.Abstractions.Time;

public class Clock
{
    public static DateTime CurrentDate() => DateTime.UtcNow;

    public static Date CurrentDateObject() => new Date(CurrentDate());
}