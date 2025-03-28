using KeycloakAuthIntegration.Application.CQRS.Users.SynchronizeUser;
using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Domain.Interfaces.Services;
using Mapster;
using MediatR;

namespace KeycloakAuthIntegration.Application.Services;

public class UserSyncService(IMediator mediator) : IUserSyncService
{
    public async Task<bool> SynchronizeUser(UserSynchronized command, CancellationToken cancellationToken)
    {
        var userSynchronizedCommand = command.Adapt<SynchronizeUserCommand>();
        return (await mediator.Send(userSynchronizedCommand, cancellationToken)).Success;
    }
}