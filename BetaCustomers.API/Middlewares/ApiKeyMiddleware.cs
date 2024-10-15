using BetaCustomers.API.Config;
using Microsoft.Extensions.Options;

namespace BetaCustomers.API.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_NAME = "X-API-KEY";
    private readonly UsersApiConfig _usersApiConfig;

    public ApiKeyMiddleware(RequestDelegate next, 
                            IOptions<UsersApiConfig> options)
    {
        _next = next;
        _usersApiConfig = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip API Key validation for token generation endpoint
        var path = context.Request.Path.Value;
        if (path.StartsWith("/api/Auth/authenticate"))
        {
            await _next(context);
            return;
        }

        // Check if the request contains the API Key in the header
        if (!context.Request.Headers.TryGetValue(API_KEY_NAME, out var apiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is missing.");
            return;
        }
        
        // Validate the API key against a configured value 
        var extractApiKey = _usersApiConfig.ApiKey;
        if (!apiKey.Equals(extractApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is missing.");
            return;
        }
        
        // If the API key is valid, proceed to the next middleware
        await _next(context);
    }

}

public static class ApiMiddlewareExtensions
{
    public static IApplicationBuilder UseApiMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyMiddleware>();
    }
}