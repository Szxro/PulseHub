using PulseHub.SharedKernel;

namespace PulseHub.Domain.Contracts;

public interface IEmailService
{
    string GenerateCode(int length = 10);

    Task SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}
