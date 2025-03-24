using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models.Dtos;
using Microsoft.Extensions.Configuration;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class AuthRequestHandler(IConfiguration configuration) : IAuthRequestHandler
{
    public AuthRequestDataDto GetAuthRequestData(string grantType)
    {
        var clientId = configuration.GetSection("Keycloak:resource")
                       ?? throw new InvalidOperationException("Keycloak:resource não encontrado.");

        if (string.IsNullOrEmpty(clientId.Value) || string.IsNullOrWhiteSpace(clientId.Value))
            throw new ArgumentException("Keycloak:resource não pode ser vazio ou nulo.", nameof(clientId));

        var clientSecret = configuration.GetSection("Keycloak:credentials:secret")
                           ?? throw new InvalidOperationException("Keycloak:credentials:secret não encontrado.");

        if (string.IsNullOrEmpty(clientSecret.Value) || string.IsNullOrWhiteSpace(clientSecret.Value))
            throw new ArgumentException("Keycloak:credentials:secret não pode ser vazio ou nulo.",
                nameof(clientSecret));
        
        return new AuthRequestDataDto(grantType, clientId.Value, clientSecret.Value);
    }
}