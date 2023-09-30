namespace Meetings4IT.Shared.Implementations.Options;

public class IntegrationLogOptions
{
    public bool Enabled { get; set; }
    public string LogConnection { get; set; } = default!;
}