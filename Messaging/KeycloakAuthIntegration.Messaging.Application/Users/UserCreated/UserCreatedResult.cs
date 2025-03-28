using KeycloakAuthIntegration.Common.Messaging.Enums;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserCreated;

public record UserCreatedResult(Guid Id, SyncStatus Status);