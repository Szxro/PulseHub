namespace PulseHub.Domain.Contracts;

public interface IEmailCodeRepository
{
    Task<string?> GetEmailCodeByUsernameAndEmail(string username, string email, CancellationToken cancellationToken = default);
}
