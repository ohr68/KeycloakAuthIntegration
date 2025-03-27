using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Requests;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class RoleService(IRealmHandler realmHandler, IKeycloakClientHandler keycloakClientHandler, IRoleRequests roleRequests) : IRoleService
{
    public async Task<RoleRepresentation> GetRoleByNameAsync(string? roleName, CancellationToken cancellationToken)
      => await roleRequests.GetRoleByNameAsync(realmHandler.GetRealm(), keycloakClientHandler.GetClientUuid(), roleName ?? Roles.DefaultUmaProtection, cancellationToken);
}