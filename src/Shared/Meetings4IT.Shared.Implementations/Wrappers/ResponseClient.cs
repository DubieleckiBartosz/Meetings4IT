namespace Meetings4IT.Shared.Implementations.Wrappers;

public class ResponseClient<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
}