namespace Panels.Application.Options;

public class OutboxPanelOptions
{
    public bool Enabled { get; set; }
    public bool DeleteAfter { get; set; }
    public TimeSpan? StartDelay { get; set; }
    public TimeSpan? Interval { get; set; }
}