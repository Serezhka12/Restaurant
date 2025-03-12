using System.Text.Json;
using Api.Dtos;

namespace Api.Middleware;

public class ApiResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiResponseMiddleware> _logger;

    public ApiResponseMiddleware(RequestDelegate next, ILogger<ApiResponseMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred processing the request.");
            throw;
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }

        if (context.Response.StatusCode == 204)
        {
            context.Response.StatusCode = 200;
            var emptyResponse = ApiResponse<object>.Success(null, 200);
            await context.Response.WriteAsJsonAsync(emptyResponse);
            return;
        }

        responseBody.Seek(0, SeekOrigin.Begin);
        var responseContent = await new StreamReader(responseBody).ReadToEndAsync();

        if (!context.Response.HasStarted &&
            !string.IsNullOrEmpty(context.Response.ContentType) &&
            context.Response.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase) &&
            context.Response.StatusCode != 404)
        {
            if (!string.IsNullOrEmpty(responseContent))
            {
                try
                {
                    var data = JsonSerializer.Deserialize<object>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    var apiResponse = ApiResponse<object>.Success(data, context.Response.StatusCode);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(apiResponse);
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "Error deserializing JSON response.");
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            else
            {
                var apiResponse = ApiResponse<object>.Success(null, context.Response.StatusCode);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(apiResponse);
            }
        }
        else
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}

public static class ApiResponseMiddlewareExtensions
{
    public static IApplicationBuilder UseApiResponseMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiResponseMiddleware>();
    }
}