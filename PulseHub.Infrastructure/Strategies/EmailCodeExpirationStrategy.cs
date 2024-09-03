using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;

namespace PulseHub.Infrastructure.Strategies;

public class EmailCodeExpirationStrategy : IExpiredStrategy<EmailCode>
{
    private readonly IEmailCodeRepository _emailCodeRepository;

    public EmailCodeExpirationStrategy(IEmailCodeRepository emailCodeRepository)
    {
        _emailCodeRepository = emailCodeRepository;
    }
    public async Task<List<EmailCode>> GetExpiredEntitiesAsync(DateTime currentDateTime, CancellationToken cancellationToken = default)
    {
        return await _emailCodeRepository.GetExpiredEmailCodesAsync(currentDateTime, cancellationToken);
    }

    public void MarkEntitiesAsExpired(List<EmailCode> entities)
    {
        entities.ForEach(e => e.IsExpired = true);

        _emailCodeRepository.UpdateRange(entities);
    }
}