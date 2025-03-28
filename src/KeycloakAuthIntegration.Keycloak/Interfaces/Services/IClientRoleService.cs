using KeycloakAuthIntegration.Keycloak.Models;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IClientRoleService
{
    Task<RoleRepresentation?> GetRoleByNameAsync(string? roleName, CancellationToken cancellationToken);
}