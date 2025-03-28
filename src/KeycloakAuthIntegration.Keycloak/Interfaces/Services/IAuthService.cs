using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakAuthIntegration.Keycloak.Models.Responses;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponse> AuthenticateAsync(AuthRequest request, CancellationToken cancellationToken);
    Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken);
}