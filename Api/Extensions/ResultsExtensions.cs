namespace Api.Extensions;

public static class ResultsExtensions
{
    public static IResult ApiResponse<T>(this IResultExtensions resultExtensions, T data, int statusCode = 200)
    {
        return Results.Json(Dtos.ApiResponse<T>.Success(data, statusCode), statusCode: statusCode);
    }

    public static IResult ApiError(this IResultExtensions resultExtensions, string error, int statusCode)
    {
        return Results.Json(Dtos.ApiResponse<object>.Fail(error, statusCode), statusCode: statusCode);
    }
}