using Microsoft.AspNetCore.Authentication;

using PulseHub.Api.Scheme.Options;

namespace PulseHub.Api.Scheme.Events;

public class HeaderReceivedContext : ResultContext<AccessKeyAuthenticationSchemeOptions>
{
    public HeaderReceivedContext(
        HttpContext context,
        AuthenticationScheme scheme,
        AccessKeyAuthenticationSchemeOptions options) : base(context, scheme, options)
    {
    }

    public string? AccessKey { get; set; }

    public string? ApplicationName { get; set; }
}
