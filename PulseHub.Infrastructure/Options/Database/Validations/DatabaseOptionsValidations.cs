﻿using FluentValidation;

namespace PulseHub.Infrastructure.Options.Database.Validations;

public class DatabaseOptionsValidations : AbstractValidator<DatabaseOptions>
{
    public DatabaseOptionsValidations()
    {
        RuleFor(x => x.ConnectionString)
            .NotNull().WithMessage("The {PropertyName} can't be null")
            .NotEmpty().WithMessage("The {PropertyName} can't be empty");

        RuleFor(x => x.CommandTimeout)
            .NotNull().WithMessage("The {PropertyName} can't be null")
            .NotEmpty().WithMessage("The {PropertyName} can't be empty")
            .GreaterThan(0).WithMessage("The {PropertyName} must be greather than 0");

        RuleFor(x => x.EnableDetailedErrors)
            .NotNull().WithMessage("The {PropertyName} can't be null")
            .NotEmpty().WithMessage("The {PropertyName} can't be empty");

        RuleFor(x => x.EnableSensitiveDataLogging)
            .NotNull().WithMessage("The {PropertyName} can't be null")
            .NotEmpty().WithMessage("The {PropertyName} can't be empty");
    }
}
