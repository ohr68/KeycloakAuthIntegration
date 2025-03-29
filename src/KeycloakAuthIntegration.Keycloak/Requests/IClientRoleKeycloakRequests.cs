using KeycloakAuthIntegration.Keycloak.Interfaces;
using KeycloakAuthIntegration.Keycloak.Models;
using Refit;

namespace KeycloakAuthIntegration.Keycloak.Requests;

public interface IClientRoleKeycloakRequests : IKeycloakRequest
{
    [Headers("Content-Type: application/json")]
    [Get("/admin/realms/{realm}/clients/{clientId}/roles/{roleName}")]
    Task<RoleRepresentation> GetRoleByNameAsync(string realm, string clientId, string roleName, CancellationToken cancellationToken);
}