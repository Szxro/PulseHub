﻿using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Infrastructure.Options.SmtpServer;

public class SmtpServerOptions : IOptionSetup
{
    public string SectionName => "SmtpServerOptions";

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool UseSsl { get; set; } = false;

    public string FromAddress { get; set; } = string.Empty;
}
