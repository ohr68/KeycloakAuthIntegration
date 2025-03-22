using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakAuthIntegration.Keycloak.Models.Responses;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponse> AuthenticateAsync(AuthRequest request);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
}