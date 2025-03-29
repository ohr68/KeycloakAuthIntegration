using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Enums;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Saga.CreateUser.Commands;
using MediatR;

namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser.Handlers;

public class AssignClientRoleCommandHandler(
    IClientRoleService clientRoleService, 
    IUserService userService) 
    : IRequestHandler<AssignClientRoleCommand>
{
    public async Task Handle(AssignClientRoleCommand request, CancellationToken cancellationToken)
    {
        var clientRole = await clientRoleService.GetRoleByNameAsync(ClientRoles.DefaultUmaProtection, cancellationToken)
                         ?? throw new Exception($"Failed to get client role {ClientRoles.DefaultUmaProtection}");

        await userService.AssignRoleAsync(request.UserId, new List<RoleRepresentation> { clientRole }, RoleType.Client, cancellationToken);
    }
}