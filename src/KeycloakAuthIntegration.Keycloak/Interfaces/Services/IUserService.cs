using KeycloakAuthIntegration.Keycloak.Enums;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Models.Dtos;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IUserService
{
    Task<bool> CreateUserAsync(UserRepresentation user, CancellationToken cancellationToken);
    Task<bool> AssignRoleAsync(string userId, IEnumerable<RoleRepresentation> role, RoleType roleType, CancellationToken cancellationToken);
    Task<UserRepresentation?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task DeleteUserAsync(string userId, CancellationToken cancellationToken);
}