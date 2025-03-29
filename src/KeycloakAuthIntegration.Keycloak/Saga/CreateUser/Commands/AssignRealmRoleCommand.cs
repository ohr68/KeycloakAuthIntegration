using MediatR;

namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser.Commands;

public class AssignRealmRoleCommand(string userId) : IRequest
{
    public string UserId { get; set; } = userId;
}
