using System.Net;
using Cloupard.Domain.Results;
using ILogger = Serilog.ILogger;

namespace Cloupard.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            await HandeExceptionAsync(context, ex);
        }
    }

    private async Task HandeExceptionAsync(HttpContext context, Exception exception)
    {
        var response = exception switch
        {
            /*
             * other exceptions
             */
            
            UnauthorizedAccessException => Result.Error((int)HttpStatusCode.Unauthorized, "Unauthorized"),
            
            // default
            _ => Result.Error((int)HttpStatusCode.InternalServerError, "Server error")
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}