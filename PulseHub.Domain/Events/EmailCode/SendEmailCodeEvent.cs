﻿using PulseHub.SharedKernel;

namespace PulseHub.Domain.Events.EmailCode;

public class SendEmailCodeEvent : DomainEvent
{
    public SendEmailCodeEvent(string username, string email)
    {
        Username = username;
        Email = email;
    }

    public string Username { get; set; }

    public string Email { get; set; }
}
