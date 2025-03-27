using MediatR;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.Logout;

public class LogoutCommand : IRequest<LogoutResult>
{
    public Guid UserId { get; set; }
}