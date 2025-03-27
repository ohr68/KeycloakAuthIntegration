using KeycloakAuthIntegration.Messaging.Domain.Enums;

namespace KeycloakAuthIntegration.Messaging.Domain.Entities;

public class UserSync
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public SyncStatus Status { get; set; }

    public void Synchronized()
    {
        UpdatedAt = DateTime.UtcNow;
        Status = SyncStatus.Completed;
    }
}