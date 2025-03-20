
using MassTransit;
using Pacagroup.Ecommerce.Application.Interface.Infrastructure;

namespace Pacagroup.Ecommerce.Infrastructure.EventBus;

public class EventBusRabbitMQ : IEventBus
{
    private readonly IPublishEndpoint publishEndpoint;

    public EventBusRabbitMQ(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    public async void Publish<T>(T @event)
    {
        await publishEndpoint.Publish(@event);
    }
}
