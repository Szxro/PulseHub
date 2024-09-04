using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Persistence;

namespace PulseHub.Infrastructure.Services;

public class DatabaseInitializerService : IDatabaseInitializerService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<DatabaseInitializerService> _logger;

    public DatabaseInitializerService(
        AppDbContext dbContext,
        ILogger<DatabaseInitializerService> logger,
        IHostApplicationLifetime hostApplicationLifetime)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task CanConnectAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            bool isConnected = await _dbContext.Database.CanConnectAsync(cancellationToken);

            if (!isConnected)
            {
                _logger.LogWarning("Cant connect to the database, database not available");

                return;
            }

            _logger.LogInformation("Successfully connect to the database!!!.");

        }
        catch (Exception ex)
        {
            _logger.LogError(
                "An error happen trying to connected to the database {providerName} with the error message: {message}",
                _dbContext.Database.ProviderName,
                ex.Message);

            throw;
        }
    }

    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Database.MigrateAsync(cancellationToken);

            _logger.LogInformation("Successfully apply the migrations!!.");

        }
        catch (Exception ex)
        {
            _logger.LogError(
                "An error happen trying to apply migrations to the database {providerName} with the error message: {message}",
                _dbContext.Database.ProviderName,
                ex.Message);

            throw;
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (await _dbContext.Provider.AnyAsync(cancellationToken))
            {
                return;
            }

            Provider[] providers = [
                new Provider
                {
                    Name = "Discord",
                    Description = "Discord is a platform for text, voice, and video chat, designed for creating and managing communities and staying connected.",
                },
                new Provider
                {
                    Name = "Telegram",
                    Description = "Telegram is a messaging app that offers fast, secure text, voice, and video communication. It supports group chats, channels, and multimedia sharing.",
                }];

            _dbContext.Provider.AddRange(providers);

            await _dbContext.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            _logger.LogError(
                "An unexpected error happen while tryint to seed some entities with the error message : {message}",
                ex.Message);

            throw;
        }
    }
}
