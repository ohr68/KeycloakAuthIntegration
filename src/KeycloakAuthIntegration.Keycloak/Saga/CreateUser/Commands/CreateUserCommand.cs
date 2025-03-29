using KeycloakAuthIntegration.Keycloak.Models;
using MediatR;

namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser.Commands;

public class CreateUserCommand(UserRepresentation user) : IRequest<UserRepresentation>
{
    public UserRepresentation User { get; set; } = user;
}