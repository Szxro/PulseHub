using PulseHub.SharedKernel;

namespace PulseHub.Domain.Errors;

public class ApplicationErrors
{
    public static Error ApplicationNameNotUnique(string name)
        => Error.Conflit($"An Application with the name '{name}' is already registered.");
}
