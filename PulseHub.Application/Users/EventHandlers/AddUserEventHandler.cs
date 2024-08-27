using Microsoft.Extensions.Logging;
using PulseHub.Domain.Events.User;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Application.Users.EventHandlers
{
    internal class AddUserEventHandler : IDomainEventHandler<AddUserEvent>
    {
        private readonly ILogger<AddUserEventHandler> _logger;

        public AddUserEventHandler(ILogger<AddUserEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(AddUserEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "A new user was register with the username: '{username}' and with the email address : '{email}'",
                notification.Username,
                notification.Email);

            return Task.CompletedTask;
        }
    }
}
