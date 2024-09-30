using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using PulseHub.Api.Scheme.Events;
using PulseHub.Api.Scheme.Options;
using PulseHub.Domain.Contracts;
using DomainApplication = PulseHub.Domain.Entities.Application;

namespace PulseHub.Api.Scheme.Handler;

public class AccessKeyAuthenticationHandler : AuthenticationHandler<AccessKeyAuthenticationSchemeOptions>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public AccessKeyAuthenticationHandler(
        IOptionsMonitor<AccessKeyAuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IServiceScopeFactory scopeFactory) 
        : base(options, logger, encoder)
    {
        _scopeFactory = scopeFactory;
    }

    // The handler calls methods on the events which give the application control at certain points where processing is occurring
    protected new AccessKeyEvents Events
    {
        get { return (AccessKeyEvents)base.Events!; }
        set { base.Events = value; }
    }

    protected override Task<object> CreateEventsAsync() => Task.FromResult<object>(new AccessKeyEvents());

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? accessKey = null;
        string? applicationName = null;

        // Given an opportunity to verify if the access key and application name is in another location
        HeaderReceivedContext headerReceivedContext = new HeaderReceivedContext(Context, Scheme, Options);

        await Events.OnHeaderReceivedContext(headerReceivedContext);

        if (headerReceivedContext.Result is not null)
        {
            return headerReceivedContext.Result;
        }

        // If retrieved the accesKey or applicationName use that
        accessKey = headerReceivedContext.AccessKey;
        applicationName = headerReceivedContext.ApplicationName;
        

        // if one or both are empty or null , retrieved the access key and applicationName from the Request.Query
        if (string.IsNullOrEmpty(accessKey) 
            || string.IsNullOrEmpty(applicationName))
        {
            // if the current request query dont contain the accessKey or name indicates that not enough information was provided
            if (!Request.Query.ContainsKey("accessKey") && !Request.Query.ContainsKey("name"))
            {
                return AuthenticateResult.NoResult();
            }

            accessKey = Request.Query["accessKey"];
            applicationName = Request.Query["name"];
        }

        using IServiceScope scope = _scopeFactory.CreateScope();

        IApplicationRepository applicationRepository = scope.ServiceProvider.GetRequiredService<IApplicationRepository>();

        IAccessKeyService accessKeyService = scope.ServiceProvider.GetRequiredService<IAccessKeyService>();

        // Trying to fetch the application and current active keys (by default can be just one active)
        DomainApplication? foundApplication = await applicationRepository.GetApplicationAndAccessKeyByNameAsync(applicationName!);

        if (foundApplication is null )
        {
            return AuthenticateResult.Fail("Application not found.");
        }

        string? foundCipherKey = foundApplication.AccessKeys.Select(x => x.EncryptedKey).FirstOrDefault();

        if (string.IsNullOrEmpty(foundCipherKey))
        {
            return AuthenticateResult.Fail("Access key not found.");
        }

        string decipherKey = accessKeyService.DecryptAccessKey(foundCipherKey!);

        // Checking equality between keys with the FixedTimeEquals to prevent time attacks
        try
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(accessKey!);

            byte[] decryptedBytes = Encoding.UTF8.GetBytes(decipherKey);

            if (!CryptographicOperations.FixedTimeEquals(plainTextBytes, decryptedBytes))
            {
                return AuthenticateResult.Fail("Access Key do not match!!!.");
            }

        } catch
        {
            return AuthenticateResult.Fail("Access key or decrypted key is not in a valid format.");
        }

        // Creating the claims of the current request
        Claim[] claims = { 
            new Claim(ClaimTypes.Name, foundApplication.Name),
            new Claim(ClaimTypes.Sid, decipherKey)
        };

        // Creating the claims principal
        ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "AccessKey"));

        // Creating the authentication ticket
        AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        ChallengeContext challengeContext = new ChallengeContext(Context, Scheme, Options);

        Response.StatusCode = StatusCodes.Status401Unauthorized;

        return Events.OnChallengeContext(challengeContext);
    }

    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        ForbiddenContext forbiddenContext = new ForbiddenContext(Context, Scheme, Options);

        Response.StatusCode = StatusCodes.Status403Forbidden;

        return Events.OnForbiddentContext(forbiddenContext);
    }
}
