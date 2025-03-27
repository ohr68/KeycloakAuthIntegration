namespace KeycloakAuthIntegration.Common.Messaging.Interfaces;

public interface IQueueService
{
    Task Publish(object message, CancellationToken cancellationToken);
    Task Send(object message, CancellationToken cancellationToken);
}