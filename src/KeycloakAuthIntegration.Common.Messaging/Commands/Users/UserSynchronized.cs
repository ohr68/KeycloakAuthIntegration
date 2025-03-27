using KeycloakAuthIntegration.Common.Messaging.Enums;

namespace KeycloakAuthIntegration.Common.Messaging.Commands.Users;

public class UserSynchronized : Message
{
    public Guid Id { get; set; }
    public SyncStatus Status { get; set; }
}