using Meetings4IT.Shared.Abstractions.Time;

namespace Meetings4IT.Shared.Abstractions.Kernel.ValueObjects;

public class DateRange : ValueObject
{
    private TimeSpan Duration => (EndDate.HasValue ? EndDate.Value - StartDate : TimeSpan.Zero);
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; }

    public DateRange(DateTime startDate, DateTime? endDate)
    {
        if (endDate.HasValue && endDate.Value < startDate)
        {
            throw new ArgumentException("EndDate must be greater than StartDate.");
        }

        if (startDate < Clock.CurrentDate())
        {
            throw new ArgumentException("StartDate must be younger than the current date.");
        }

        StartDate = startDate;
        EndDate = endDate;
    }

    public int DurationInMinutes => Duration.Minutes;
    public int DurationInHours => Duration.Hours;

    public override string ToString()
    {
        var endDate = EndDate.HasValue ? $"-{EndDate.Value:T}" : string.Empty;

        return $"{StartDate:d} {StartDate:T}{endDate}";
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}