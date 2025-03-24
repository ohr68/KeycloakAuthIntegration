using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakAuthIntegration.Keycloak.Models.Responses;
using Mapster;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.RefreshToken;

public class RefreshTokenProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RefreshTokenCommand, RefreshTokenRequest>();
        config.NewConfig<RefreshTokenResponse, RefreshTokenResult>()
            .ConstructUsing(a => new RefreshTokenResult(a.AccessToken, TimeSpan.FromMinutes(a.ExpiresIn),
                TimeSpan.FromMinutes(a.RefreshTokenExpiresIn), a.RefreshToken, a.TokenType, a.NotValidBeforePolicy,
                a.SessionState, a.Scope));
    }
}