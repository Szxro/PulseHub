using FluentValidation;

namespace PulseHub.Infrastructure.Options.Hashing.Validations;

public class HashingOptionsValidations : AbstractValidator<HashingOptions>
{
    public HashingOptionsValidations()
    {
        RuleFor(x => x.Iterations)
            .NotEmpty().WithMessage("The {PropertyName} can't empty")
            .NotNull().WithMessage("The {PropertyName} can't null")
            .GreaterThan(10000).WithMessage("The {PropertyName} must be greater than 10000");

        RuleFor(x => x.SaltSize)
            .NotEmpty().WithMessage("The {PropertyName} can't empty")
            .NotNull().WithMessage("The {PropertyName} can't null")
            .GreaterThan(0).WithMessage("The {PropertyName} must be greater than 0");

        RuleFor(x => x.HashSize)
            .NotEmpty().WithMessage("The {PropertyName} can't empty")
            .NotNull().WithMessage("The {PropertyName} can't null")
            .GreaterThan(0).WithMessage("The {PropertyName} must be greater than 0");
    }
}
