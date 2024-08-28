using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PulseHub.Infrastructure.Options.Database;
using PulseHub.Infrastructure.Options.Hashing;
using PulseHub.Infrastructure.Options.SmtpServer;
using PulseHub.Infrastructure.Validators;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Infrastructure.Extensions;

public static partial class InfrastructureExtensions
{
    public static IServiceCollection AddConfigurableOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>()
                .AddFluentValidator<DatabaseOptions>();

        services.ConfigureOptions<HashingOptionsSetup>()
                .AddFluentValidator<HashingOptions>();

        services.ConfigureOptions<SmtpServerOptionsSetup>()
                .AddFluentValidator<SmtpServerOptions>();

        return services;
    }

    private static IServiceCollection AddFluentValidator<TOption>(this IServiceCollection services)
        where TOption : class,IOptionSetup
    {
        services.AddSingleton<IValidateOptions<TOption>>(
            provider => new FluentOptionsValidator<TOption>(provider.GetRequiredService<IServiceScopeFactory>()));

        return services;
    }
}
