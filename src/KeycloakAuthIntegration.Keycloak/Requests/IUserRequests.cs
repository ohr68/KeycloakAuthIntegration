using KeycloakAuthIntegration.Keycloak.Interfaces;
using KeycloakAuthIntegration.Keycloak.Models;
using Refit;

namespace KeycloakAuthIntegration.Keycloak.Requests;

public interface IUserRequests : IRequest
{
    [Headers("Content-Type: application/json")]
    [Post("/admin/realms/{realm}/users")]
    Task<ApiResponse<object?>> CreateUserAsync(string realm, [Body] UserRepresentation user, CancellationToken cancellationToken);
    
    [Headers("Content-Type: application/json")]
    [Post("/admin/realms/{realm}/users/{userId}/role-mappings/clients/{clientUuid}")]
    Task<ApiResponse<object?>> AssignRoleAsync(string realm, string userId, string clientUuid, [Body] IEnumerable<RoleRepresentation> role, CancellationToken cancellationToken);
    
    [Headers("Content-Type: application/json")]
    [Get("/admin/realms/{realm}/users?exact=true")]
    Task<IEnumerable<UserRepresentation>> GetByUsernameAsync(string realm, [Query] string username, CancellationToken cancellationToken);
}