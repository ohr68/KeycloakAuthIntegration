using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Requests;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class RealmRoleService(IRealmHandler realmHandler, IRealmRoleRequests realmRoleRequests) : IRealmRoleService
{
    public async Task<RoleRepresentation?> GetRoleByNameAsync(string? roleName, CancellationToken cancellationToken)
       => await realmRoleRequests.GetRoleByNameAsync(realmHandler.GetRealm(), roleName ?? RealmRoles.Admin, cancellationToken);
}