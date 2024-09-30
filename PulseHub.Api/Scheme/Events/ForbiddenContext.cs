using Microsoft.AspNetCore.Authentication;

using PulseHub.Api.Scheme.Options;

namespace PulseHub.Api.Scheme.Events;

public class ForbiddenContext : ResultContext<AccessKeyAuthenticationSchemeOptions>
{
    public ForbiddenContext(
        HttpContext context,
        AuthenticationScheme scheme,
        AccessKeyAuthenticationSchemeOptions options) : base(context, scheme, options)
    {
    }
}
