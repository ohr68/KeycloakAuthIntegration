using KeycloakAuthIntegration.Keycloak.Models.Dtos;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IAuthRequestHandler
{
    AuthRequestDataDto GetAuthRequestData(string grantType);
}