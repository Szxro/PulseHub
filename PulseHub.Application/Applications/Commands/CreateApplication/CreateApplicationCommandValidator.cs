using FluentValidation;

namespace PulseHub.Application.Applications.Commands.CreateApplication;

public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
{
    public CreateApplicationCommandValidator()
    {
        RuleFor(x => x.name)
            .NotEmpty().WithMessage("The application name can't be empty.")
            .NotNull().WithMessage("The application name can't be null.")
            .MinimumLength(3).WithMessage("The application name must contain at least three characters.")
            .MaximumLength(20).WithMessage("The application name must not exceed 20 characters.")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("The application name can only contain letters, numbers, and underscores.");

        RuleFor(x => x.description)
            .NotEmpty().WithMessage("The application description can't be empty.")
            .NotNull().WithMessage("The application description can't be null.")
            .MinimumLength(15).WithMessage("The application description must contain at least fifteen characters.")
            .MaximumLength(35).WithMessage("The application description must not exceed 35 characters.");

        RuleFor(x => x.provider)
            .NotEmpty().WithMessage("The provider can't be empty.")
            .NotNull().WithMessage("The provider description can't be null.");

        RuleFor(x => x.providerApplicationId)
            .NotEmpty().WithMessage("The application provider application id can't be empty.")
            .NotNull().WithMessage("The application provider application id can't be null.");
    }
}
