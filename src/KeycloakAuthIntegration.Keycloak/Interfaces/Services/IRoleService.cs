using KeycloakAuthIntegration.Keycloak.Models;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IRoleService
{
    Task<RoleRepresentation> GetRoleByNameAsync(string? roleName, CancellationToken cancellationToken);
}