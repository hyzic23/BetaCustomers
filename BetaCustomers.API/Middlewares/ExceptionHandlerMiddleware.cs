using BetaCustomers.API.Models;

namespace BetaCustomers.API.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // Handle exception
            _logger.LogError($"{ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError($"An unexpected error occurred {exception} ");
        BaseResponse errorResponse = exception switch
        {
            ApplicationException _ => new BaseResponse(StatusCodes.Status400BadRequest, "Application exception occured"),
            KeyNotFoundException _ => new BaseResponse(StatusCodes.Status404NotFound, "The request key not found"),
            UnauthorizedAccessException _ => new BaseResponse(StatusCodes.Status401Unauthorized, "Unauthorized"),
            _ => new BaseResponse(StatusCodes.Status500InternalServerError, "Internal server error. Please retry later")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorResponse.StatusCode;
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}