using PulseHub.Domain.Entities;

namespace PulseHub.Domain.Contracts;

public interface IEmailCodeRepository : IRepositoryWriter<EmailCode>
{
    Task<string?> GetEmailCodeByUsernameAndEmail(string username, string email, CancellationToken cancellationToken = default);

    Task<EmailCode?> GetEmailCodeByCode(string emailCode, CancellationToken cancellationToken = default);

    Task<EmailCode?> GetCurrentActiveEmailCodeByUsernameAndEmailAsync(string username, string email, CancellationToken cancellationToken = default);

    Task<bool> IsUserEmailCodeVerifiedByUsernameAndEmailAsync(string username,string email, CancellationToken cancellationToken = default);

    Task<List<EmailCode>> GetExpiredEmailCodesAsync(DateTime currentTime, CancellationToken cancellationToken = default);
}
