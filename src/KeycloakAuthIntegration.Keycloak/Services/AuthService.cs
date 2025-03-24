using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakAuthIntegration.Keycloak.Models.Responses;
using KeycloakAuthIntegration.Keycloak.Requests;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class AuthService(IRealmHandler realmHandler, IAuthRequests authRequests) : IAuthService
{
    public async Task<AuthResponse> AuthenticateAsync(AuthRequest request, CancellationToken cancellationToken)
        => await authRequests.LoginAsync(realmHandler.GetRealm(), request, cancellationToken);
    
    public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        => await authRequests.RefreshTokenAsync(realmHandler.GetRealm(), request, cancellationToken);
}