namespace KeycloakAuthIntegration.Common.Messaging.Messaging;

public class Message
{
    public Guid CorrelationId { get; private set; } = Guid.NewGuid();
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}