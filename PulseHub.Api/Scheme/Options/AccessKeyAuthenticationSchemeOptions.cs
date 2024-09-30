using Microsoft.AspNetCore.Authentication;

using PulseHub.Api.Scheme.Events;

namespace PulseHub.Api.Scheme.Options;

public class AccessKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public new AccessKeyEvents Events
    {
        get { return (AccessKeyEvents)base.Events!; }
        set { base.Events = value; }
    }
}
