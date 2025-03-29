using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Requests;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class RealmRoleService(IRealmHandler realmHandler, IRealmRoleKeycloakRequests realmRoleKeycloakRequests) : IRealmRoleService
{
    public async Task<RoleRepresentation?> GetRoleByNameAsync(string? roleName, CancellationToken cancellationToken)
       => await realmRoleKeycloakRequests.GetRoleByNameAsync(realmHandler.GetRealm(), roleName ?? RealmRoles.Admin, cancellationToken);
}