using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Events.User;
using PulseHub.SharedKernel.Contracts;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Templates;

namespace PulseHub.Application.Users.EventHandlers
{
    internal class SendEmailCodeHandler : IDomainEventHandler<SendEmailCodeEvent>
    {
        private readonly ILogger<SendEmailCodeHandler> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEmailService _emailService;

        public SendEmailCodeHandler(
            ILogger<SendEmailCodeHandler> logger,
            IServiceScopeFactory scopeFactory,
            IEmailService emailService)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _emailService = emailService;
        }
        public async Task Handle(SendEmailCodeEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "A new user was register with the username: '{username}' and with the email address : '{email}'",
                notification.Username,
                notification.Email);

            using IServiceScope scope = _scopeFactory.CreateScope();

            IEmailCodeRepository emailCodeRepository = scope.ServiceProvider.GetRequiredService<IEmailCodeRepository>();

            string? emailCode = await emailCodeRepository.GetEmailCodeByUsernameAndEmail(notification.Username, notification.Email, cancellationToken);

            await _emailService.SendEmailAsync(
                new EmailMessage { 
                    ToAddress = notification.Email,
                    Subject = "PulseHub Email Verification Code",
                    Body = EmailTemplates.GetVerificationEmailBodyHtml(notification.Username,emailCode!)
                },cancellationToken);
        }
    }
}
