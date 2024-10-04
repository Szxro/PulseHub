using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using FluentValidation.Results;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Infrastructure.Options.Validators;

public class FluentOptionsValidator<TOption> : IValidateOptions<TOption>
    where TOption : class, IOptionSetup
{
    private readonly IServiceScopeFactory _scopeFactory;

    public FluentOptionsValidator(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public ValidateOptionsResult Validate(string? name, TOption options)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        IValidator<TOption> validator = scope.ServiceProvider.GetRequiredService<IValidator<TOption>>();

        ValidationResult result = validator.Validate(options);

        if (result.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        List<string> errors = result.Errors.Select(x => $"Validation failed to {x.PropertyName} with the Error Message {x.ErrorMessage}")
                                           .ToList();

        return ValidateOptionsResult.Fail(errors);
    }
}
