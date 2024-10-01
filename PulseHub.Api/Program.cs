using PulseHub.Api.Extensions;
using PulseHub.Application;
using PulseHub.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    // Adding Serilog
    builder.Host.UseSerilog((host, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(host.Configuration));

    // Add services to the container.
    builder.Services
            .AddPresentation()
            .AddApplication()
            .AddInfrastructure(builder.Environment);

    builder.Configuration.AddUserSecrets<Program>(optional:false,reloadOnChange:true);
}


WebApplication app = builder.Build();
{ 
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseExceptionHandler();

    app.UseCors("default");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}