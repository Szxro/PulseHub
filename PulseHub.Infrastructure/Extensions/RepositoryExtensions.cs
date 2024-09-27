using Microsoft.Extensions.DependencyInjection;
using PulseHub.Domain.Contracts;
using PulseHub.Infrastructure.Repositories;

namespace PulseHub.Infrastructure.Extensions;

public static partial class InfrastructureExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IEmailCodeRepository, EmailCodeRepository>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IApplicationRepository, ApplicationRepository>();

        services.AddScoped<IProviderRepository, ProviderRepository>();
            
        return services;
    }
}
