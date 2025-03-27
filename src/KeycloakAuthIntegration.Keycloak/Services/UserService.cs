using System.Text.Json;
using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Models.Dtos;
using KeycloakAuthIntegration.Keycloak.Requests;
using KeycloakIntegration.Common.Exceptions;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class UserService(
    IRoleService roleService,
    IRealmHandler realmHandler,
    IKeycloakClientHandler keycloakClientHandler,
    IUserRequests userRequests) : IUserService
{
    public async Task<bool> CreateUserAsync(UserRepresentation user, CancellationToken cancellationToken)
        => (await userRequests.CreateUserAsync(realmHandler.GetRealm(), user, cancellationToken)).IsSuccessStatusCode;

    public async Task<bool> AssignRoleAsync(Guid userId, IEnumerable<RoleRepresentation> role,
        CancellationToken cancellationToken)
        => (await userRequests.AssignRoleAsync(realmHandler.GetRealm(), userId.ToString(),
            keycloakClientHandler.GetClientUuid(), role, cancellationToken)).IsSuccessStatusCode;

    public async Task<UserRepresentation> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        => (await userRequests.GetByUsernameAsync(realmHandler.GetRealm(), username, cancellationToken))
           .FirstOrDefault()
           ?? throw new NotFoundException("User not found");

    public async Task CreateUserFlowAsync(CreateUserFlowDto dto, CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting CreateUserFlow for user {0}", dto.UserId);

        var createdResult = await CreateUserAsync(dto.User, cancellationToken);

        if (!createdResult)
            throw new BadRequestException("Failed to create user.");

        var createdUser = await GetByUsernameAsync(dto.User.Username!, cancellationToken)
                          ?? throw new NotFoundException(
                              $"Failed to get user Id: {dto.UserId} Name: {dto.User.Username!}.");

        var role = await roleService.GetRoleByNameAsync(Roles.DefaultUmaProtection, cancellationToken)
                   ?? throw new NotFoundException($"Failed to get role: {Roles.DefaultUmaProtection}.");

        var assignRoleResult = await AssignRoleAsync(Guid.Parse(createdUser.Id!), new List<RoleRepresentation> { role }, cancellationToken);

        if (!assignRoleResult)
            throw new BadRequestException("Failed to assign role.");
    }
}