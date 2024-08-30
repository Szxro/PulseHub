using PulseHub.SharedKernel;
using System.Text.RegularExpressions;

namespace PulseHub.Domain.ValueObjects;

public class Email : ValueObject
{
    public const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static bool IsValid(string email,out Result<Email>? validEmail)
    {
        validEmail = null;

        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        if (!Regex.IsMatch(email,EmailPattern,RegexOptions.CultureInvariant))
        {
            return false;
        }

        validEmail = Result.Success(new Email(email));
        return true;
    }

    public static Error InvalidEmail => Error.Validation(string.Empty, "InvalidEmail.Error", "Invalid email address. Please check and try again.");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
