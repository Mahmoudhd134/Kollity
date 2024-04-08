using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Kollity.Services.API.Extensions;

public static class ResultExtensions
{
    public static IResult ToIResult(this Result result)
    {
        return result.IsSuccess
            ? Results.Empty
            : result.ToProblemDetails();
    }

    public static IResult ToIResult<T>(this Result<T> result)
    {
        return result.IsSuccess
            ? result.Data is not null ? Results.Ok(result.Data) : Results.Empty
            : result.ToProblemDetails();
    }

    public static IResult ToProblemDetails(this Result result)
    {
        return Results.Problem(
            statusCode: GetStatusCode(result.Errors.First().Type),
            title: GetTitle(result.Errors.First().Type),
            type: GetType(result.Errors.First().Type),
            extensions: new Dictionary<string, object>
            {
                { "errors", result.Errors }
            }
        );
    }

    public static FailureType ToFailureType(this Result result)
    {
        return new FailureType
        {
            Status = GetStatusCode(result.Errors.First().Type),
            Title = GetTitle(result.Errors.First().Type),
            Type = GetType(result.Errors.First().Type),
            Errors = result.Errors
        };
    }

    public static ActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.Errors.Any())
            return new ObjectResult(result.ToFailureType());
        return new OkObjectResult(result.Data);
    }

    private static string GetType(ErrorType type)
    {
        return type switch
        {
            ErrorType.Failure => "https://tools.ietf.org/html/rfc9110#section-15.6.1",
            ErrorType.Validation => "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            ErrorType.NotFound => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            ErrorType.Conflict => "https://tools.ietf.org/html/rfc9110#section-15.5.10",
            _ => "https://tools.ietf.org/html/rfc9110#section-15.6.1"
        };
    }

    private static string GetTitle(ErrorType type)
    {
        return type switch
        {
            ErrorType.Failure => "Internal Server Error",
            ErrorType.Validation => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            _ => "Internal Server Error"
        };
    }

    private static int GetStatusCode(ErrorType type)
    {
        return type switch
        {
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}