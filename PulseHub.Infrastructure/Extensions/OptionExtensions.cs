using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using PulseHub.Infrastructure.Options.Aes;
using PulseHub.Infrastructure.Options.Database;
using PulseHub.Infrastructure.Options.Hashing;
using PulseHub.Infrastructure.Options.Jwt;
using PulseHub.Infrastructure.Options.MediaStorage;
using PulseHub.Infrastructure.Options.SmtpServer;
using PulseHub.Infrastructure.Options.Validators;
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

        services.ConfigureOptions<JwtOptionsSetup>()
                .AddFluentValidator<JwtOptions>();

        services.ConfigureOptions<AesOptionSetup>()
                .AddFluentValidator<AesOptions>();

        services.ConfigureOptions<MediaStorageOptionsSetup>()
                .AddFluentValidator<MediaStorageOptions>();

        services.ConfigureOptions<JwtBearerOptionsSetup>();

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
