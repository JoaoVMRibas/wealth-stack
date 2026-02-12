using System.Net;
using System.Text.Json;
using WealthStack.Domain.Exceptions;

namespace WealthStack.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = exception.Message;

        if(exception is DomainException or ApplicationException)
        {
            code = HttpStatusCode.BadRequest;
        }

        if(exception.Message.Contains("not found",StringComparison.OrdinalIgnoreCase))
        {
            code = HttpStatusCode.NotFound;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        var response = JsonSerializer.Serialize(new { error = result });
        return context.Response.WriteAsync(response);
    }
}