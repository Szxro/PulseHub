﻿using System.Diagnostics.CodeAnalysis;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.SharedKernel;

public class Result : IResult
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    protected Result(
        bool isSuccess,
        Error error)
    {
        if (isSuccess && error != Error.None
            || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error {error}",nameof(error));
        }

        Error = error;
        IsSuccess = isSuccess;
    }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value,bool isSuccess,Error error)
        :base(isSuccess,error)
    {
        _value = value;
    }

    // Accessing the value of the generic result (is going to throw an exception if its a failure)
    [NotNull]
    public TValue Value => IsSuccess ? 
        _value! 
        : throw new ApplicationException("The value of a failure result can't be accessed.");

    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    // If hide was intended  need to use the new keyword 
    public static new Result<TValue> Failure(Error error) => new(default, false, error);

    public static Result<TValue> ValidationFailure(Error error) => new(default, false, error);
}