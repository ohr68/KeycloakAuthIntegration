using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Enums;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Saga.CreateUser.Commands;
using MediatR;

namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser.Handlers;

public class AssignRealmRoleHandler(
    IRealmRoleService realmRoleService, 
    IUserService userService) 
    : IRequestHandler<AssignRealmRoleCommand>
{
    public async Task Handle(AssignRealmRoleCommand request, CancellationToken cancellationToken)
    {
        var realmRole = await realmRoleService.GetRoleByNameAsync(RealmRoles.Admin, cancellationToken)
                        ?? throw new Exception($"Failed to get realm role {RealmRoles.Admin}");

        await userService.AssignRoleAsync(request.UserId, new List<RoleRepresentation> { realmRole }, RoleType.Realm, cancellationToken);
    }
}