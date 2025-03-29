using KeycloakAuthIntegration.Caching.Extensions;
using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Requests;
using Microsoft.Extensions.Caching.Distributed;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class SessionService(
    IRealmHandler realmHandler,
    ISessionKeycloakRequests sessionKeycloakRequests) : ISessionService
{
    public async Task<bool> LogoutAsync(string userId, CancellationToken cancellationToken)
        => (await sessionKeycloakRequests.LogoutAsync(realmHandler.GetRealm(), userId, cancellationToken)).IsSuccessStatusCode;
}