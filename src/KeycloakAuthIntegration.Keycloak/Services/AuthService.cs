using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakAuthIntegration.Keycloak.Models.Responses;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class AuthService : IAuthService
{
    public Task<AuthResponse> AuthenticateAsync(AuthRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        throw new NotImplementedException();
    }
}