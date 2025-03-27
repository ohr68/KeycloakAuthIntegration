using KeycloakAuthIntegration.Common.Messaging.Enums;

namespace KeycloakAuthIntegration.Messaging.Commands;

public class UserSynchronizedCommand
{
    public Guid Id { get; set; }
    public SyncStatus Status { get; set; }
}