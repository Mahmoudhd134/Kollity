using Domain.ErrorHandlers;

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
            ? result.Data is null ? Results.Ok(result.Data) : Results.Empty
            : result.ToProblemDetails();
    }

    private static IResult ToProblemDetails(this Result result)
    {
        return Results.Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: "Bad Request",
            extensions: new Dictionary<string, object>
            {
                { "errors", result.Errors }
            }
        );
    }
}