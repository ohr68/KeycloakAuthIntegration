using KeycloakAuthIntegration.Common.Messaging.Enums;
using MediatR;

namespace KeycloakAuthIntegration.Application.CQRS.Users.SynchronizeUser;

public class SynchronizeUserCommand : IRequest<SynchronizeUserResult>
{
    public Guid Id { get; set; }
    public SyncStatus Status { get; set; }
    public UserSyncOperation Operation { get; set; }
}