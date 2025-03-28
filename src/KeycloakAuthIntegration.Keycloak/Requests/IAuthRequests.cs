using KeycloakAuthIntegration.Keycloak.Interfaces;
using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakAuthIntegration.Keycloak.Models.Responses;
using Refit;

namespace KeycloakAuthIntegration.Keycloak.Requests;

public interface IAuthRequests : IRequest
{
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    [Post("/realms/{realm}/protocol/openid-connect/token")]
    Task<AuthResponse> LoginAsync(string realm, [Body(BodySerializationMethod.UrlEncoded)] AuthRequest request, CancellationToken cancellationToken);

    [Headers("Content-Type: application/x-www-form-urlencoded")]
    [Post("/realms/{realm}/protocol/openid-connect/token")]
    Task<RefreshTokenResponse> RefreshTokenAsync(string realm, [Body(BodySerializationMethod.UrlEncoded)] RefreshTokenRequest request, CancellationToken cancellationToken);
}