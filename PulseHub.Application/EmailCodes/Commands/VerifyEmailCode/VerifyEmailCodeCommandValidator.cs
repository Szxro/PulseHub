using FluentValidation;

namespace PulseHub.Application.EmailCodes.Commands.VerifyEmailCode;

public class VerifyEmailCodeCommandValidator : AbstractValidator<VerifyEmailCodeCommand>
{
    public VerifyEmailCodeCommandValidator()
    {
        RuleFor(x => x.code)
            .NotNull().WithMessage("The email code can't be null")
            .NotEmpty().WithMessage("The email code can't be empty")
            .MaximumLength(10).WithMessage("The email code must not exceed 10 characters.");
    }
}
