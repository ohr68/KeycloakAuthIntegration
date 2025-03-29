using KeycloakAuthIntegration.Keycloak.Interfaces;
using KeycloakAuthIntegration.Keycloak.Models;
using Refit;

namespace KeycloakAuthIntegration.Keycloak.Requests;

public interface IRealmRoleKeycloakRequests : IKeycloakRequest
{
    [Headers("Content-Type: application/json")]
    [Get("/admin/realms/{realm}/roles/{roleName}")]
    Task<RoleRepresentation> GetRoleByNameAsync(string realm, string roleName, CancellationToken cancellationToken);
}