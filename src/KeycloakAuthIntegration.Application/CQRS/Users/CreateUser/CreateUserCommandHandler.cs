using MediatR;

namespace KeycloakAuthIntegration.Application.CQRS.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    public Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}