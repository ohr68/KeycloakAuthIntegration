using KeycloakAuthIntegration.Messaging.Interfaces;
using MassTransit;

namespace KeycloakAuthIntegration.Messaging.Services;

public class QueueService(IBus bus): IQueueService
{
    public async Task Publish(object message, CancellationToken cancellationToken = default) 
        => await bus.Publish(message, cancellationToken);

    public async Task Send(object message, CancellationToken cancellationToken = default) 
        => await bus.Send(message, cancellationToken);
}