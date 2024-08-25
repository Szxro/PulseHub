using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Infrastructure.Options.Base;

public abstract class BaseSetup<TOption> : IConfigureOptions<TOption>
    where TOption : class, IOptionSetup
{
    private readonly IConfiguration _configuration;

    public BaseSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(TOption options)
    {
        _configuration
            .GetSection(options.SectionName)
            .Bind(options);
    }
}
