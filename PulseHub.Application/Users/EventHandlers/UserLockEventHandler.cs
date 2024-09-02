using PulseHub.Domain.Contracts;
using PulseHub.Domain.Events.Users;
using PulseHub.SharedKernel.Contracts;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Templates;
using Microsoft.Extensions.Logging;

namespace PulseHub.Application.Users.EventHandlers;

public class UserLockEventHandler : IDomainEventHandler<UserLockOutEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<UserLockOutEvent> _logger;

    public UserLockEventHandler(
        IEmailService emailService,
        ILogger<UserLockOutEvent> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }
    public async Task Handle(UserLockOutEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning(
            "Sending a lockout notification for a user with the username '{username}' and with the email '{email}'",
            notification.Username,
            notification.Email);

        await _emailService.SendEmailAsync(
            new EmailMessage
            {
                Subject = "Your Account Has Been Temporarily Locked",
                Body = EmailTemplates.GetLockOutEmailBodyHtml(notification.Username,DateTime.Now),
                ToAddress = notification.Email
            });
    }
}
