using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Requests;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class UserService(IRealmHandler realmHandler, IUserRequests userRequests) : IUserService
{
    public async Task<bool> CreateUserAsync(UserRepresentation user)
      => (await userRequests.CreateUserAsync(realmHandler.GetRealm(), user)).IsSuccessful;
}