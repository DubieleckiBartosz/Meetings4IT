using Meetings4IT.Shared.Abstractions.Exceptions;
using Newtonsoft.Json;

namespace Meetings4IT.API.Common;

public class ErrorHandlingMiddleware
{
    private const string ServerError = "Server Error";

    private readonly RequestDelegate _next;
    private readonly Serilog.ILogger _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, Serilog.ILogger logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this._next(context);
        }
        catch (Exception e)
        {
            _logger.Error($"Handling error: {e.Message}");

            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        var response = CreateErrorResponse(exception, statusCode);

        var errorResponse = JsonConvert.SerializeObject(response);

        _logger.Error($"Error details: {errorResponse}");

        await httpContext.Response.WriteAsync(errorResponse);
    }

    private int GetStatusCode(Exception exception) =>
        exception switch
        {
            ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,
            ArgumentNullException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            BaseException e => e.StatusCode == null ? StatusCodes.Status500InternalServerError : (int)e.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

    public static IReadOnlyList<string>? AssignErrors(Exception exception)
    {
        IReadOnlyList<string>? errors = null;

        if (exception is ValidationErrorListException validationErrorListException)
        {
            errors = validationErrorListException.Errors;
        }

        return errors;
    }

    private object CreateErrorResponse(Exception exception, int statusCode)
    {
        var title = string.Empty;

        if (exception is BaseException baseException)
        {
            title = baseException.Title;
        }

        var response = new
        {
            title = title ?? ServerError,
            status = statusCode,
            detail = exception.Message,
            errors = AssignErrors(exception)
        };

        return response;
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}