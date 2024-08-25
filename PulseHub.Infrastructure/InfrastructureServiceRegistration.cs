using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PulseHub.Infrastructure.Extensions;
using PulseHub.Infrastructure.Options.Database;
using PulseHub.Infrastructure.Persistence;

namespace PulseHub.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(InfrastructureServiceRegistration).Assembly);

        services.AddConfigurableOptions();

        services.AddDbContext<AppDbContext>((provider,options) => 
        {
            DatabaseOptions databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            options.UseSqlServer(databaseOptions.ConnectionString,sqlOptions =>
            {
                sqlOptions.CommandTimeout(databaseOptions.CommandTimeout);

            })
            .UseSnakeCaseNamingConvention();

            // Enable it in development
            options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
        });

        return services;
    }
}
