using BetaCustomers.API.Models;
using BetaCustomers.API.Utils;

namespace BetaCustomers.API.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        var token = context.Request
            .Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ")[1];
        if (token == null)
        {
            //check if incoming request is from an enabled unauthorized route
            if (IsEnabledUnauthorizedRoute(context))
            {
                return _next(context);
            }

            BaseResponse response = new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Unauthorized"));
            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(response);

        }
        else
        {
            if (JwtUtils.ValidateJwtToken(token))
            {
                return _next(context);
            }
            else
            {
                BaseResponse response = new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Unauthorized"));
                context.Response.StatusCode = response.StatusCode;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsJsonAsync(response);
            }
        }
    }


    ///  <summary>
    ///  This method is used to check if incoming request is from an enabled unauthorized requests
    ///  </summary>
    ///  <param name="context"></param>
    ///  <returns></returns>
    private static bool IsEnabledUnauthorizedRoute(HttpContext context)
    {
        var enabledRoutes = new List<string>
        {
            "/api/Auth/authenticate",
            "/api/Users/create"
        };

        var isEnableUnauthorizedRoute = false;
        if (context.Request.Path.Value is not null)
        {
            isEnableUnauthorizedRoute = enabledRoutes.Contains(context.Request.Path.Value);
        }

        return isEnableUnauthorizedRoute;
    }
    
    
}

public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}