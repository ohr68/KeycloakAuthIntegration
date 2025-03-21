using MediatR;

namespace KeycloakAuthIntegration.Application.CQRS.Auth;

public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResult>
{
    public Task<AuthResult> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}