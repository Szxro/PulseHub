using PulseHub.Api.Middlewares;

namespace PulseHub.Api.Extensions;

public static partial class ApiExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddAuthentication().AddAccessKeyAuth().AddJwtBearer();
        services.AddAuthorization();

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
                policy.SetIsOriginAllowed(origin => true);
            });
        });

        return services;
    }
}
