
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Pacagroup.Ecommerce.Infrastructure.EventBus.Options;

public class RabbitMQOptionsSetup : IConfigureOptions<RabbitMQOptions>
{
    private const string ConfigurationSectionName = "RabbitMQOptions";
    private readonly IConfiguration configuration;

    public RabbitMQOptionsSetup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void Configure(RabbitMQOptions options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
