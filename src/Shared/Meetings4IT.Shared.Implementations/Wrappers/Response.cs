namespace Meetings4IT.Shared.Implementations.Wrappers;

public class Response
{
    public bool Success { get; }
    public string? Message { get; }

    protected Response(string? message, bool success)
    {
        Message = message;
        Success = success;
    }

    public static Response Ok()
    {
        return new Response(null, true);
    }

    public static Response Ok(string? message)
    {
        return new Response(message, true);
    }

    public static Response Error(string message)
    {
        return new Response(message, false);
    }
}

public class Response<T> : Response
{
    public T Data { get; set; }

    private Response(T data, bool success, string? message) : base(message, success)
    {
        Data = data;
    }

    public static Response<T> Error(T data, string message)
    {
        return new Response<T>(data, false, message);
    }

    public static Response<T> Error(T data)
    {
        return new Response<T>(data, false, null);
    }
}