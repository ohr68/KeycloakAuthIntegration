using KeycloakAuthIntegration.Common.Messaging.Interfaces;
using MassTransit;

namespace KeycloakAuthIntegration.Common.Messaging.Services;

public class QueueService(IBus bus): IQueueService
{
    public async Task Publish(object message, CancellationToken cancellationToken = default) 
        => await bus.Publish(message, cancellationToken);

    public async Task Send(object message, CancellationToken cancellationToken = default) 
        => await bus.Send(message, cancellationToken);
}