using KeycloakAuthIntegration.Keycloak.Models.Dtos;
using KeycloakAuthIntegration.Keycloak.Saga.CreateUser;

namespace KeycloakAuthIntegration.Keycloak.Saga.Interfaces.CreateUser;

public interface ICreateUserSaga
{
    Task Start(CreateUserFlowDto dto, CancellationToken cancellationToken);
    Task Complete(string userId, CancellationToken cancellationToken);
    Task HandleError(CreateUserSagaState sagaState, string errorMessage, CancellationToken cancellationToken);
}