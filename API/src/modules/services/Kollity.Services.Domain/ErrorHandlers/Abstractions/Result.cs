namespace Kollity.Services.Domain.ErrorHandlers.Abstractions;

public class Result
{
    protected Result(bool isSuccess, List<Error> errors)
    {
        if (isSuccess && errors != null)
            throw new InvalidOperationException();

        if (!isSuccess && errors == null)
            throw new InvalidOperationException();


        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; init; }
    public List<Error> Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, new[] { error }.ToList());
    }

    public static Result Failure(List<Error> errors)
    {
        return new Result(false, errors);
    }

    public static implicit operator Result(Error error)
    {
        return Failure(error);
    }

    public static implicit operator Result(List<Error> errors)
    {
        return Failure(errors);
    }
}

public class Result<TResult> : Result
{
    private Result(bool isSuccess, TResult result, List<Error> errors) : base(isSuccess, errors)
    {
        Data = result;
    }

    public TResult Data { get; init; }

    public static Result<TResult> Success(TResult result)
    {
        return new Result<TResult>(true, result, null);
    }

    public static Result<TResult> Failure(Error error)
    {
        return new Result<TResult>(false, default,
            new[] { error }.ToList());
    }

    public static Result<TResult> Failure(List<Error> errors)
    {
        return new Result<TResult>(false, default, errors);
    }


    public static implicit operator Result<TResult>(Error error)
    {
        return Failure(error);
    }

    public static implicit operator Result<TResult>(List<Error> errors)
    {
        return Failure(errors);
    }

    public static implicit operator Result<TResult>(TResult value)
    {
        return Success(value);
    }
}