using System.Net;
using DotnetCleanArch.Application.Exceptions;
using DotnetCleanArch.Infrastructure.Exceptions;

namespace DotnetCleanArch.API.Middleware;

public class ErrorHandler
{
    private readonly RequestDelegate _next;

    public ErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "text/plain";

        switch (exception)
        {
            case BusinessException businessEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync(businessEx.Message);

            case InternalException internalEx:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync(internalEx.Message);

            case ExternalException externalEx:
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                return context.Response.WriteAsync(externalEx.Message);

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync("An unexpected fault happened. Try again later.");
        }
    }
}