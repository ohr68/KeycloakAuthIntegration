using System.Text.Json;
using KeycloakAuthIntegration.Keycloak.Configuration;
using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakIntegration.Common.Exceptions;
using Microsoft.Extensions.Options;

namespace KeycloakAuthIntegration.Keycloak.RequestHandlers;

public class ConsumerAuthHeaderHandler(IAuthRequestHandler authRequestHandler, IAuthService authService, IOptions<KeycloakUserOptions> keycloakUser)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var authData = authRequestHandler.GetAuthRequestData(GrantType.Password);
        var token = await authService.AuthenticateAsync(
            new AuthRequest
            {
                GrantType = authData.GrantType,
                ClientId = authData.ClientId,
                ClientSecret = authData.ClientSecret,
                Username = keycloakUser.Value.Username,
                Password = keycloakUser.Value.Password
            },
            cancellationToken);

        if (token == null)
            throw new BadRequestException("Falha ao autenticar");

        request.Headers.Add("Authorization", $"Bearer {token.AccessToken}");

        return await base.SendAsync(request, cancellationToken);
    }
}