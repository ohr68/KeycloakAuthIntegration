namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface ISessionService
{
    Task<bool> LogoutAsync(string realm, CancellationToken cancellationToken);
}