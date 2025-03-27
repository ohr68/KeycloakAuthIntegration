using System.Security.Claims;

namespace KeycloakAuthIntegration.Keycloak.Interfaces.Services;

public interface IJwtService
{
    IEnumerable<Claim> GetClaimsFromJwt(string token);
    string GetClaimValueFromJwt(string token, string claimType);
}