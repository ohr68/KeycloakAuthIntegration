namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IKeycloakClientHandler
{
    string GetClientId();
    string GetClientUuid();
}