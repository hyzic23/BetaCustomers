namespace BetaCustomers.API.Middleware;

public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Log information about the incoming request
        var requestPath = context.Request.Path;
        var requestMethod = context.Request.Method;
        Console.WriteLine($"Incoming request: {requestMethod} {requestPath}");
        
        // Call the next Middleware in the pipeline
        await _next(context);
        
        // Middleware code to execute after the request has been handled
    }
}