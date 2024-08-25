using PulseHub.SharedKernel.Contracts;

namespace PulseHub.SharedKernel;

public sealed class Result : IResult
{
    public bool IsSuccess { get; }

    public bool IsFailure { get; }

    public Error Error { get; }

    public List<Error> ValidationErrors { get; }

    private Result(
        Error error,
        bool isSuccess,
        List<Error> validationErrors)
    {
        if (isSuccess && error != Error.None
            || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error {error}",nameof(error));
        }

        Error = error;
        IsSuccess = isSuccess;
        IsFailure = !isSuccess;
        ValidationErrors = validationErrors;
    }

    public static Result Success = new(Error.None,true,new List<Error>(0));

    public static Result Failure(Error error) => new(error,false,new List<Error>(0));

    public static Result ValidationFailure(List<Error> validationErrors) => new(Error.Validation,false,validationErrors); 
}

public class Result<TValue> : IResult
{
    public TValue? Value { get; }

    public bool IsSuccess { get; }

    public bool IsFailure { get; }

    public Error Error { get; }

    public List<Error> ValidationErrors { get; }

    public Result(
        TValue? value,
        bool isSuccess,
        Error error,
        List<Error> validationErrors)
    {
        if (isSuccess && error != Error.None
         || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error {error}", nameof(error));
        }

        Value = value;
        IsSuccess = isSuccess;
        IsFailure = !isSuccess;
        Error = error;
        ValidationErrors = validationErrors;
    }

    public static Result<TValue> Success(TValue value) => new(value,true,Error.None,new List<Error>(0));

    public static Result<TValue> Failure(Error error) => new(default,false,error,new List<Error>(0));

    public static Result<TValue> ValidationFailure(List<Error> validationErrors) => new(default,false,Error.Validation,validationErrors);
}