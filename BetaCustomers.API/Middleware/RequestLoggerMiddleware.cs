namespace BetaCustomers.API.Middleware;

public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggerMiddleware> _logger;

    public RequestLoggerMiddleware(RequestDelegate next, 
                                   ILogger<RequestLoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // Log information about the incoming request
        var requestPath = context.Request.Path;
        var requestMethod = context.Request.Method;
        _logger.LogInformation($"Incoming request: {requestMethod} {requestPath}");
        
        // Call the next Middleware in the pipeline
        await _next(context);
        
        // Middleware code to execute after the request has been handled
    }
}