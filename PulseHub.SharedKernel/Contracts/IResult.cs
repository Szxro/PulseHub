namespace PulseHub.SharedKernel.Contracts;

public interface IResult
{
    public bool IsSuccess { get; }

    public bool IsFailure { get; }

    public Error Error { get; }

    public List<Error> ValidationErrors { get; }
}
