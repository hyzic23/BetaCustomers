using BetaCustomers.API.Config;
using BetaCustomers.API.Models;
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

    public Task InvokeAsync(HttpContext context)
    {
        // Skip API Key validation for token generation endpoint
        var path = context.Request.Path.Value;
        if (path.StartsWith("/api/auth/authenticate"))
        {
            return _next(context);
        }

        // Check if the request contains the API Key in the header
        if (!context.Request.Headers.TryGetValue(API_KEY_NAME, out var apiKey))
        {
            var response = new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Unauthorized"));
            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(response);
        }
        
        // Validate the API key against a configured value 
        var extractApiKey = _usersApiConfig.ApiKey;
        if (!apiKey.Equals(extractApiKey))
        {
            var response = new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Unauthorized"));
            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(response);
        }
        
        // If the API key is valid, proceed to the next middleware
        return _next(context);
    }

}

public static class ApiMiddlewareExtensions
{
    public static IApplicationBuilder UseApiMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyMiddleware>();
    }
}