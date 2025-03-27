using KeycloakAuthIntegration.Messaging.Domain.Enums;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserCreated;

public record UserCreatedResult(Guid Id, string? Status);