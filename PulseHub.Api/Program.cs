using PulseHub.Api.Extensions;
using PulseHub.Application;
using PulseHub.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
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

    app.UseHttpsRedirection();

    app.UseExceptionHandler();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}