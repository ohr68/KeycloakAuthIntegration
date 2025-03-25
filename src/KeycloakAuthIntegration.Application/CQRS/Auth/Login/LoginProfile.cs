using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakAuthIntegration.Keycloak.Models.Responses;
using Mapster;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.Login;

public class LoginProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginCommand, AuthRequest>();
        config.NewConfig<AuthResponse, LoginResult>()
            .ConstructUsing(a => new LoginResult(a.AccessToken, TimeSpan.FromSeconds(a.ExpiresIn),
                TimeSpan.FromSeconds(a.RefreshTokenExpiresIn), a.RefreshToken, a.TokenType, a.NotValidBeforePolicy,
                a.SessionState, a.Scope));
    }
}