using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Models.Dtos;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IUserService
{
    Task<bool> CreateUserAsync(UserRepresentation user, CancellationToken cancellationToken);
    Task<bool> AssignRoleAsync(Guid userId, IEnumerable<RoleRepresentation> role, CancellationToken cancellationToken);
    Task<UserRepresentation> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task CreateUserFlowAsync(CreateUserFlowDto dto, CancellationToken cancellationToken);
}