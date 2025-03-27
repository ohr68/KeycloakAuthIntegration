using KeycloakAuthIntegration.Common.Messaging.Messaging;

namespace KeycloakIntegration.Common.Messaging.Users;

public class UserCreated : Message
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
}