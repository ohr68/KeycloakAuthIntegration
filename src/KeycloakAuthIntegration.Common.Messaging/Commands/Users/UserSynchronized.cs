using KeycloakAuthIntegration.Common.Messaging.Enums;

namespace KeycloakAuthIntegration.Common.Messaging.Commands.Users;

public class UserSynchronized : Message
{
    public Guid Id { get; set; }
    public SyncStatus Status { get; set; }
    public UserSyncOperation Operation { get; set; }

    public UserSynchronized()
    {
        
    }
    
    public UserSynchronized(Guid id, SyncStatus status, UserSyncOperation operation)
    {
        Id = id;
        Status = status;
        Operation = operation;
    }
}