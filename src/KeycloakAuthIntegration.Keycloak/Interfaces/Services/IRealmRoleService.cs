using KeycloakAuthIntegration.Keycloak.Models;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IRealmRoleService
{
    Task<RoleRepresentation?> GetRoleByNameAsync(string? roleName, CancellationToken cancellationToken);
}