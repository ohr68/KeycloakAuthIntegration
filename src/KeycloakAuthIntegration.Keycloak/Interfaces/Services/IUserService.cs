using KeycloakAuthIntegration.Keycloak.Models;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IUserService
{
    Task<bool> CreateUserAsync(UserRepresentation user);
}