using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PulseHub.Infrastructure.Extensions;
using PulseHub.Infrastructure.Options.Database;
using PulseHub.Infrastructure.Persistence;
using PulseHub.Infrastructure.Persistence.Interceptors;

namespace PulseHub.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,IHostEnvironment environment)
    {
        services.AddValidatorsFromAssembly(typeof(InfrastructureServiceRegistration).Assembly);

        services.AddConfigurableOptions();

        services.AddInterceptors();

        services.AddDbContext<AppDbContext>((provider,options) => 
        {
            DatabaseOptions databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            options.UseSqlServer(databaseOptions.ConnectionString,sqlOptions =>
            {
                sqlOptions.CommandTimeout(databaseOptions.CommandTimeout);

            })
            .AddInterceptors(provider.GetRequiredService<EnforceEmailCodeInterceptor>())
            .AddInterceptors(provider.GetRequiredService<AuditableEntityInterceptor>())
            .AddInterceptors(provider.GetRequiredService<DomainEventInterceptor>())
            .UseSnakeCaseNamingConvention();

            if (environment.IsDevelopment())
            {
                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            }
        });

        services.AddChannels();

        services.AddServices();

        services.AddWorkers();

        services.AddRepositories();

        return services;
    }
}
