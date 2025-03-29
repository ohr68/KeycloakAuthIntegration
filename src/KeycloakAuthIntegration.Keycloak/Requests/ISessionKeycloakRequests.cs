using KeycloakAuthIntegration.Keycloak.Interfaces;
using Refit;

namespace KeycloakAuthIntegration.Keycloak.Requests;

public interface ISessionKeycloakRequests : IKeycloakRequest
{
    [Post("/admin/realms/{realm}/users/{userId}/logout")]
    Task<ApiResponse<object?>> LogoutAsync(string realm, string userId, CancellationToken cancellationToken);
}