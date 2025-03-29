using KeycloakAuthIntegration.Keycloak.Models.Dtos;
using MediatR;

namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser;

public class CreateUserSagaRequest(CreateUserFlowDto userDto) : IRequest
{
    public CreateUserFlowDto UserDto { get; } = userDto;
}