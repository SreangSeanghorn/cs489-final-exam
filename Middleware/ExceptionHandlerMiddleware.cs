using AstronautSatelliteAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AstronautSatelliteAPI.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
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
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex is AstronautNotFoundException ? StatusCodes.Status404NotFound : StatusCodes.Status500InternalServerError;

            var response = new
            {
                timestamp = DateTime.UtcNow,
                status = context.Response.StatusCode,
                error = ex.GetType().Name,
                message = ex.Message,
                path = context.Request.Path
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}