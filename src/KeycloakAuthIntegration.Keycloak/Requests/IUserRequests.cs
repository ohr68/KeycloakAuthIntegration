using KeycloakAuthIntegration.Keycloak.Interfaces;
using KeycloakAuthIntegration.Keycloak.Models;
using Refit;

namespace KeycloakAuthIntegration.Keycloak.Requests;

public interface IUserRequests: IRequest
{
    [Post("/auth/admin/realms/{realm}/users")]
    Task<ApiResponse<object?>> CreateUserAsync(string realm, [Body] UserRepresentation user);
}