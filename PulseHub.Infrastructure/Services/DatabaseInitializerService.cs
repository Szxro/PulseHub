using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.Infrastructure.Persistence;

namespace PulseHub.Infrastructure.Services;

public class DatabaseInitializerService : IDatabaseInitializerService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<DatabaseInitializerService> _logger;

    public DatabaseInitializerService(
        AppDbContext dbContext,
        ILogger<DatabaseInitializerService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task CanConnectAsync()
    {
        try
        {
            bool isConnected = await _dbContext.Database.CanConnectAsync();

            if (!isConnected)
            {
                _logger.LogWarning("Cant connect to the database, database not available");

                return;
            }

            _logger.LogInformation("Successfully connect to the database!!!.");

        } catch (Exception ex)
        {
            _logger.LogError(
                "An error happen trying to connected to the database {providerName} with the error message: {message}",
                _dbContext.Database.ProviderName,
                ex.Message);

            throw;
        }
    }

    public async Task MigrateAsync()
    {
        try
        {
            await _dbContext.Database.MigrateAsync();

            _logger.LogInformation("Successfully apply the migrations!!.");

        } catch (Exception ex)
        {
            _logger.LogError(
                "An error happen trying to apply migrations to the database {providerName} with the error message: {message}",
                _dbContext.Database.ProviderName,
                ex.Message);

            throw;
        }
    }
}
