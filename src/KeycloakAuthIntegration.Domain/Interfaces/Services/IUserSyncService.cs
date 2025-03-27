using KeycloakAuthIntegration.Common.Messaging.Commands.Users;

namespace KeycloakAuthIntegration.Domain.Interfaces.Services;

public interface IUserSyncService
{
    Task<bool> SynchronizeUser(UserSynchronized command, CancellationToken cancellationToken);
}