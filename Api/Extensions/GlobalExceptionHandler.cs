using System.Net;
using Api.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Shared.Exceptions;
using Shared;

namespace Api.Extensions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unexpected error occurred");

        var (statusCode, response) = exception switch
        {
            ValidationException validationEx => HandleValidationException(validationEx),
            NotFoundException notFoundEx => HandleNotFoundException(notFoundEx),
            InvalidOperationException invalidOpEx => HandleInvalidOperationException(invalidOpEx),
            _ => HandleUnknownException(exception)
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }

    private static (int StatusCode, object Response) HandleValidationException(ValidationException exception)
    {
        var errors = exception.Errors.Select(e => new
        {
            Field = e.PropertyName,
            Error = e.ErrorMessage
        });

        return ((int)HttpStatusCode.BadRequest, ApiResponse<object>.Fail("Validation failed", (int)HttpStatusCode.BadRequest));
    }

    private static (int StatusCode, object Response) HandleNotFoundException(NotFoundException exception)
    {
        return ((int)HttpStatusCode.NotFound, ApiResponse<object>.Fail(exception.Message, (int)HttpStatusCode.NotFound));
    }

    private static (int StatusCode, object Response) HandleInvalidOperationException(InvalidOperationException exception)
    {
        return ((int)HttpStatusCode.PreconditionFailed, ApiResponse<object>.Fail(exception.Message, (int)HttpStatusCode.PreconditionFailed));
    }

    private static (int StatusCode, object Response) HandleUnknownException(Exception exception)
    {
        return ((int)HttpStatusCode.InternalServerError, ApiResponse<object>.Fail("An unexpected error occurred", (int)HttpStatusCode.InternalServerError));
    }
}