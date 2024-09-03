using Microsoft.Extensions.DependencyInjection;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Strategies;
using PulseHub.Infrastructure.Strategies.Context;

namespace PulseHub.Infrastructure.Extensions;

public static partial class InfrastructureExtensions
{
    public static IServiceCollection AddStrategies(this IServiceCollection services)
    {
        // Context
        services.AddScoped<ExpirationStrategyContext<EmailCode>>();

        services.AddScoped<ExpirationStrategyContext<RefreshToken>>();

        // Strategies
        services.AddScoped<IExpiredStrategy<EmailCode>, EmailCodeExpirationStrategy>();

        services.AddScoped<IExpiredStrategy<RefreshToken>, RefreshTokenExpirationStrategy>();

        return services;
    }
}
