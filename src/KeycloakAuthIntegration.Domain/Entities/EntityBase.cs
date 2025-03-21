namespace KeycloakAuthIntegration.Domain.Entities;

public class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public void Updated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}