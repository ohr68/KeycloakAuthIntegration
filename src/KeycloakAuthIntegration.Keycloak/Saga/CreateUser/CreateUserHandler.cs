using KeycloakAuthIntegration.Keycloak.Models.Dtos;
using KeycloakAuthIntegration.Keycloak.Saga.Interfaces.CreateUser;

namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser;

public class CreateUserHandler(ICreateUserSaga createUserSaga) : ICreateUserHandler
{
    public async Task Handle(CreateUserFlowDto dto, CancellationToken cancellationToken)
        => await createUserSaga.Start(dto, cancellationToken);
}