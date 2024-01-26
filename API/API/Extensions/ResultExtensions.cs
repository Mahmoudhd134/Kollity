using Domain.ErrorHandlers;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;

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

    private static IResult ToProblemDetails(this Result result)
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

    private static string GetType(ErrorType type) => type switch
    {
        ErrorType.Failure => "https://tools.ietf.org/html/rfc9110#section-15.6.1",
        ErrorType.Validation => "https://tools.ietf.org/html/rfc9110#section-15.5.1",
        ErrorType.NotFound => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
        ErrorType.Conflict => "https://tools.ietf.org/html/rfc9110#section-15.5.10",
        _ => "https://tools.ietf.org/html/rfc9110#section-15.6.1",
    };

    private static string GetTitle(ErrorType type) => type switch
    {
        ErrorType.Failure => "Internal Server Error",
        ErrorType.Validation => "Bad Request",
        ErrorType.NotFound => "Not Found",
        ErrorType.Conflict => "Conflict",
        _ => "Internal Server Error",
    };

    private static int GetStatusCode(ErrorType type) => type switch
    {
        ErrorType.Failure => StatusCodes.Status500InternalServerError,
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    };
}