using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Enums;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Models.Dtos;
using KeycloakAuthIntegration.Keycloak.Saga.Interfaces.CreateUser;
using KeycloakIntegration.Common.Exceptions;

namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser;

public class CreateUserSaga(
    IUserService userService,
    IClientRoleService clientRoleService,
    IRealmRoleService realmRoleService)
    : ICreateUserSaga
{
    public async Task Start(CreateUserFlowDto dto, CancellationToken cancellationToken)
    {
        var sagaState = new CreateUserSagaState
        {
            UserId = dto.UserId.ToString(),
            Username = dto.User.Username
        };

        try
        {
            Console.WriteLine("Starting CreateUserFlow for user {0}", dto.UserId);

            var createdResult = await userService.CreateUserAsync(dto.User, cancellationToken);

            if (!createdResult)
                throw new SagaFailedException(sagaState.SagaId, nameof(CreateUserSaga),
                    CreateUserSteps.GetCreatedUser.ToString(),
                    $"Failed to get created user {dto.User.Username!}",
                    $"An error occurred while attempting to get created user {dto.User.Username!}");

            sagaState.UserCreated = true;

            var createdUser = await userService.GetByUsernameAsync(dto.User.Username!, cancellationToken) ??
                              throw new SagaFailedException(sagaState.SagaId, nameof(CreateUserSaga),
                                  CreateUserSteps.GetCreatedUser.ToString(),
                                  $"Failed to get created user {dto.User.Username!}",
                                  $"An error occurred while attempting to get created user {dto.User.Username!}");

            sagaState.KeycloakUserId = createdUser.Id;

            var clientRole =
                await clientRoleService.GetRoleByNameAsync(ClientRoles.DefaultUmaProtection, cancellationToken) ??
                throw new SagaFailedException(sagaState.SagaId, nameof(CreateUserSaga),
                    CreateUserSteps.GetClientRole.ToString(),
                    $"Failed to get client role {ClientRoles.DefaultUmaProtection}",
                    $"An error occurred while attempting to get client role {ClientRoles.DefaultUmaProtection}");

            await userService.AssignRoleAsync(createdUser.Id!, new List<RoleRepresentation> { clientRole },
                RoleType.Client,
                cancellationToken);
            sagaState.ClientRoleAssigned = true;

            var realmRole = await realmRoleService.GetRoleByNameAsync(RealmRoles.Admin, cancellationToken)
                            ?? throw new SagaFailedException(sagaState.SagaId, nameof(CreateUserSaga),
                                CreateUserSteps.GetRealmRole.ToString(), $"Failed to get realm role {RealmRoles.Admin}",
                                $"An error occurred while attempting to get realm role {RealmRoles.Admin}");

            await userService.AssignRoleAsync(createdUser.Id!, new List<RoleRepresentation> { realmRole },
                RoleType.Realm,
                cancellationToken);
            sagaState.RealmRoleAssigned = true;

            await Complete(sagaState.UserId, cancellationToken);
        }
        catch (Exception ex)
        {
            await HandleError(sagaState, ex.Message, cancellationToken);
            throw new SagaFailedException(sagaState.SagaId, nameof(CreateUserSaga), "Saga failed", ex);
        }
    }

    public Task Complete(string userId, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User {userId} creation and role assignments completed successfully.");

        return Task.CompletedTask;
    }

    public async Task HandleError(CreateUserSagaState sagaState, string errorMessage,
        CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error occurred for user {sagaState.UserId}: {errorMessage}");

        if (sagaState.UserCreated && !string.IsNullOrWhiteSpace(sagaState.KeycloakUserId))
            await userService.DeleteUserAsync(sagaState.KeycloakUserId!, cancellationToken);
    }
}