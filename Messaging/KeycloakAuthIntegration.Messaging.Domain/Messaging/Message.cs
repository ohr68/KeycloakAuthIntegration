namespace KeycloakAuthIntegration.Messaging.Domain.Messaging;

public class Message
{
    public Guid CorrelationId { get; private set; }
    public DateTime Timestamp { get; private set; }
}