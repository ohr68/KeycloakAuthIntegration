using KeycloakAuthIntegration.Application.Models;
using MediatR;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.Login;

public class LoginCommand : AuthRequestModel, IRequest<LoginResult>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}