using KeycloakAuthIntegration.Keycloak.Interfaces.Services;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class SessionService(IRealmHandler realmHandler, IUserService userService) : ISessionService
{
    public Task<bool> LogoutAsync(string realm, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}