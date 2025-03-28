using KeycloakAuthIntegration.Keycloak.Models.Dtos;

namespace KeycloakAuthIntegration.Keycloak.Saga.Interfaces.CreateUser;

public interface ICreateUserHandler : ISagaHandler<CreateUserFlowDto>
{
    
}