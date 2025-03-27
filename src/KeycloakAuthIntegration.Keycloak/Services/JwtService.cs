﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakIntegration.Common.Exceptions;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class JwtService : IJwtService
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public IEnumerable<Claim> GetClaimsFromJwt(string token)
    {
        var jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
        return jwtToken.Claims;
    }

    public string GetClaimValueFromJwt(string token, string claimType)
    {
        var jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
        return jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value ??
               throw new NotFoundException($"Claim {claimType} não encontrada.");
    }
}