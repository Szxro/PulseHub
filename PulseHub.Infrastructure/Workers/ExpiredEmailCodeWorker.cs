using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;

namespace PulseHub.Infrastructure.Workers;

public class ExpiredEmailCodeWorker : BackgroundService
{
    private readonly ILogger<ExpiredEmailCodeWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private static readonly TimeSpan _timeOut = TimeSpan.FromMinutes(45);

    public ExpiredEmailCodeWorker(
        ILogger<ExpiredEmailCodeWorker> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_timeOut);

        while (await timer.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();

                IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                IEmailCodeRepository emailCodeRepository = scope.ServiceProvider.GetRequiredService<IEmailCodeRepository>();

                List<EmailCode> expiredEmailCodes = await emailCodeRepository.GetExpiredEmailCodesAsync(DateTime.Now, stoppingToken);

                if (expiredEmailCodes.Count <= 0)
                {
                    _logger.LogInformation("Not found expired email codes.");

                    continue;
                };

                _logger.LogWarning("Found {length} expired email codes, proceding to mark them as expired", expiredEmailCodes.Count);

                expiredEmailCodes.ForEach(emailCode => emailCode.IsExpired = true);

                emailCodeRepository.UpdateRange(expiredEmailCodes);

                await unitOfWork.SaveChangesAsync(stoppingToken);

            } catch(Exception ex)
            {
                _logger.LogError(
                    "An unexpected error happen while processing expired email codes with the error message : '{message}'",
                    ex.Message);
            }
        }
    }
}
