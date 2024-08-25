using FluentValidation;

namespace PulseHub.Infrastructure.Options.Database.Validations;

public class DatabaseOptionsValidations : AbstractValidator<DatabaseOptions>
{
    public DatabaseOptionsValidations()
    {
        RuleFor(x => x.ConnectionString).NotNull().WithMessage("The {PropertyName} can't be null");

        RuleFor(x => x.ConnectionString).NotEmpty().WithMessage("The {PropertyName} can't be empty");

        RuleFor(x => x.CommandTimeout).NotNull().WithMessage("The {PropertyName} can't be null");

        RuleFor(x => x.CommandTimeout).NotEmpty().WithMessage("The {PropertyName} can't be empty");

        RuleFor(x => x.CommandTimeout).Must(timeout => timeout > 0).WithMessage("The {PropertyName} must be greather than 0");

        RuleFor(x => x.EnableDetailedErrors).NotNull().WithMessage("The {PropertyName} can't be null");

        RuleFor(x => x.EnableDetailedErrors).NotEmpty().WithMessage("The {PropertyName} can't be empty");

        RuleFor(x => x.EnableSensitiveDataLogging).NotNull().WithMessage("The {PropertyName} can't be null");

        RuleFor(x => x.EnableSensitiveDataLogging).NotEmpty().WithMessage("The {PropertyName} can't be empty");
    }
}
