using KeycloakAuthIntegration.Keycloak.Enums;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Requests;
using KeycloakIntegration.Common.Exceptions;

namespace KeycloakAuthIntegration.Keycloak.Services;

public class UserService(
    IRealmHandler realmHandler,
    IKeycloakClientHandler keycloakClientHandler,
    IUserRequests userRequests) : IUserService
{
    public async Task<bool> CreateUserAsync(UserRepresentation user, CancellationToken cancellationToken)
        => (await userRequests.CreateUserAsync(realmHandler.GetRealm(), user, cancellationToken)).IsSuccessStatusCode;

    public async Task<bool> AssignRoleAsync(string keycloakUserId, IEnumerable<RoleRepresentation> role,
        RoleType roleType,
        CancellationToken cancellationToken)
    {
        var success = roleType switch
        {
            RoleType.Client => (await userRequests.AssignClientRoleAsync(realmHandler.GetRealm(), keycloakUserId,
                keycloakClientHandler.GetClientUuid(), role, cancellationToken)).IsSuccessStatusCode,
            RoleType.Realm => (await userRequests.AssignRealmRoleAsync(realmHandler.GetRealm(), keycloakUserId, role,
                cancellationToken)).IsSuccessStatusCode,
            _ => throw new InvalidOperationException("Tipo de role não suportado.")
        };

        if (!success)
            throw new BadRequestException("Failed to assign role.");

        return success;
    }

    public async Task<UserRepresentation?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        => (await userRequests.GetByUsernameAsync(realmHandler.GetRealm(), username, cancellationToken))
            .FirstOrDefault();

    public async Task<bool> UpdateUserAsync(string username, UserRepresentation user, CancellationToken cancellationToken)
    {
        var keycloakUser = await GetByUsernameAsync(username, cancellationToken)
            ?? throw new NotFoundException($"Usuário {username} não encontrado.");
        
        return (await userRequests.UpdateUserAsync(realmHandler.GetRealm(), keycloakUser.Id!, user, cancellationToken)).IsSuccessStatusCode;
    }

    public async Task DeleteUserAsync(string keycloakUserId, CancellationToken cancellationToken)
        => await userRequests.DeleteUserAsync(realmHandler.GetRealm(), keycloakUserId, cancellationToken);
}