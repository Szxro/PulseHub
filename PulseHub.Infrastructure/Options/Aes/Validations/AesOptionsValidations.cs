using FluentValidation;

namespace PulseHub.Infrastructure.Options.Aes.Validations;

public class AesOptionsValidations : AbstractValidator<AesOptions>
{
    public AesOptionsValidations()
    {
        RuleFor(aes => aes.PrivateKey)
            .NotEmpty().WithMessage("The {PropertyName} can't be empty")
            .NotNull().WithMessage("The {PropertyName} ca't be null");

        RuleFor(aes => aes.PrivateKey)
            .MinimumLength(32).WithMessage("The {PropertyName} must be a least 32 characters long")
            .MaximumLength(128).WithMessage("The {PropertyName} must be lower than 128 characters long");
    }
}
