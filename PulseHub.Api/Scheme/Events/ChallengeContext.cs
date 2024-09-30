using Microsoft.AspNetCore.Authentication;

using PulseHub.Api.Scheme.Options;

namespace PulseHub.Api.Scheme.Events;

public class ChallengeContext : ResultContext<AccessKeyAuthenticationSchemeOptions>
{
    public ChallengeContext(
        HttpContext context,
        AuthenticationScheme scheme,
        AccessKeyAuthenticationSchemeOptions options) : base(context, scheme, options)
    {
    }
}
