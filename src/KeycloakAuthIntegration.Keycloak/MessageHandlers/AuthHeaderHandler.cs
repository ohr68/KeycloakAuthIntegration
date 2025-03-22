using Microsoft.AspNetCore.Http;

namespace KeycloakAuthIntegration.Keycloak.MessageHandlers;

public class AuthHeaderHandler(IHttpContextAccessor contextAccessor) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = "teste";
        request.Headers.Add("Authorization", $"Bearer {token}");
        
        return base.SendAsync(request, cancellationToken);
    }
}