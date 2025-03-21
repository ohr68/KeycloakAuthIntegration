namespace KeycloakAuthIntegration.Application.CQRS.Auth;

public class AuthResult
{
    public string? AccessToken { get; set; }
    public TimeSpan ExpiresIn { get; set; }
    public TimeSpan RefreshTokenExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
    public string? TokenType { get; set; }
    public int NotValidBeforePolicy  { get; set; }
    public string? SessionState { get; set; }
    public string? Scope { get; set; }
}