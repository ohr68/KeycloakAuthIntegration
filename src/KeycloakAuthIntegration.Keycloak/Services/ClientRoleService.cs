using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Requests;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class ClientRoleService(IRealmHandler realmHandler, IKeycloakClientHandler keycloakClientHandler, IClientRoleKeycloakRequests clientRoleKeycloakRequests) : IClientRoleService
{
    public async Task<RoleRepresentation?> GetRoleByNameAsync(string? roleName, CancellationToken cancellationToken)
      => await clientRoleKeycloakRequests.GetRoleByNameAsync(realmHandler.GetRealm(), keycloakClientHandler.GetClientUuid(), roleName ?? ClientRoles.DefaultUmaProtection, cancellationToken);
}