
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pacagroup.Ecommerce.Application.Interface.Infrastructure;
using Pacagroup.Ecommerce.Infrastructure.EventBus;
using Pacagroup.Ecommerce.Infrastructure.EventBus.Options;

namespace Pacagroup.Ecommerce.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.ConfigureOptions<RabbitMQOptionsSetup>();
        services.AddScoped<IEventBus, EventBusRabbitMQ>();
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfq) => {

                RabbitMQOptions? opt = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitMQOptions>>().Value;

                cfq.Host(opt.HostName, opt.VirtualHost, h =>
                {
                    h.Username(opt.UserName);
                    h.Password(opt.Password);
                });

                cfq.ConfigureEndpoints(context);
            });
        });
    }
}
