using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Events.EmailCode;
using PulseHub.SharedKernel.Contracts;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Templates;

namespace PulseHub.Application.EmailCodes.EventHandlers;

internal class SendWelcomeEmailEventHandler : IDomainEventHandler<SendWelcomeEmailEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<SendWelcomeEmailEventHandler> _logger;

    public SendWelcomeEmailEventHandler(
        IEmailService emailService,
        ILogger<SendWelcomeEmailEventHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }
    public async Task Handle(SendWelcomeEmailEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Sending a welcome message to the user with the usernam '{username}' and email '{email}'",
            notification.Username,
            notification.Email);

        await _emailService.SendEmailAsync(
            new EmailMessage
            {
                ToAddress = notification.Email,
                Subject = "Welcome to PulseHub",
                Body = EmailTemplates.GetWelcomeEmailBodyHtml(notification.Username)
            }, cancellationToken);
    }
}
