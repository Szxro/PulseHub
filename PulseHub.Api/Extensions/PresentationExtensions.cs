using Microsoft.AspNetCore.SignalR;
using PulseHub.Api.Filters;
using PulseHub.Api.Middlewares;

namespace PulseHub.Api.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddAuthentication().AddAccessKeyAuth().AddJwtBearer();
        services.AddAuthorization();
        services.AddSignalR(options =>
        {
            // Global options
            options.EnableDetailedErrors = true;
            options.DisableImplicitFromServicesParameters = true;


            // Hub Filters
            options.AddFilter<GlobalLoggingHubFilter>();
        });
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddSwaggerGenWithAuth();

        // Handlers
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();


        services.AddCors(options => 
        {
            options.AddPolicy("default", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
                //policy.AllowCredentials(); (need to allow credentials when using signalR and need to define an origin)
                policy.SetIsOriginAllowed(origin => true);
            });
        });

        return services;
    }
}
