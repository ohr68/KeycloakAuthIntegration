using KeycloakAuthIntegration.Keycloak.Enums;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Models.Dtos;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IUserService
{
    Task<bool> CreateUserAsync(UserRepresentation user, CancellationToken cancellationToken);
    Task<bool> AssignRoleAsync(string keycloakUserId, IEnumerable<RoleRepresentation> role, RoleType roleType, CancellationToken cancellationToken);
    Task<UserRepresentation?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<bool> UpdateUserAsync(string username, UserRepresentation user, CancellationToken cancellationToken);
    Task DeleteUserAsync(string keycloakUserId, CancellationToken cancellationToken);
}