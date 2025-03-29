using MediatR;

namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser.Commands;

public class RollbackUserCreationCommand(CreateUserSagaState sagaState) : IRequest
{
    public CreateUserSagaState SagaState { get; set; } = sagaState;
}