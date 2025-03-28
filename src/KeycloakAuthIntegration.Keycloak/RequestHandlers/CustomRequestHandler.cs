using System.Net;
using System.Text.Json;
using KeycloakAuthIntegration.Keycloak.Models.Dtos;
using KeycloakIntegration.Common.Exceptions;

namespace KeycloakAuthIntegration.Keycloak.RequestHandlers;

public class CustomRequestHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode) return response;
        
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var keycloakError = JsonSerializer.Deserialize<KeycloakErrorDto>(content);

        var exception = response.StatusCode switch
        {
            HttpStatusCode.BadRequest => new BadRequestException(keycloakError?.Message ?? content),
            HttpStatusCode.Unauthorized => new UnauthorizedException(keycloakError?.Message ?? content),
            HttpStatusCode.Forbidden => new ForbiddenException(keycloakError?.Message ?? content),
            HttpStatusCode.NotFound => new NotFoundException(keycloakError?.Message ?? content),
            _ => new Exception(content)
        };
        
        throw exception;
    }   
}