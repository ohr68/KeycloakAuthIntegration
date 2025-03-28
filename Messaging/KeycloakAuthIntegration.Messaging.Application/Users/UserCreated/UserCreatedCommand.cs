using MediatR;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserCreated;

public class UserCreatedCommand : IRequest<UserCreatedResult>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
}