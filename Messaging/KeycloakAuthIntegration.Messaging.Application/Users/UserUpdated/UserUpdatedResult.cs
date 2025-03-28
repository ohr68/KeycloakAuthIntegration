using KeycloakAuthIntegration.Common.Messaging.Enums;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserUpdated;

public record UserUpdatedResult(Guid Id, SyncStatus Status);