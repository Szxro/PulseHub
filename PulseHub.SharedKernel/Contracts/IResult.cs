namespace PulseHub.SharedKernel.Contracts;

public interface IResult
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }
}
