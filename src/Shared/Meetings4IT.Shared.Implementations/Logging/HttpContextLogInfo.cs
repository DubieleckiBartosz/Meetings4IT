namespace Meetings4IT.Shared.Implementations.Logging;

public class HttpContextLogInfo
{
    public string? IpAddress { get; set; }
    public string Host { get; set; }
    public string Protocol { get; set; }
    public string Scheme { get; set; }
    public string User { get; set; }
}