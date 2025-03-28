using KeycloakAuthIntegration.Application.Models;
using MediatR;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.RefreshToken;

public class RefreshTokenCommand : AuthRequestModel, IRequest<RefreshTokenResult>
{
    public string? RefreshToken { get; set; }
}