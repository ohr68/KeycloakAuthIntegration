namespace KeycloakAuthIntegration.Keycloak.Models.Dtos;

public record CreateUserFlowDto(Guid UserId, UserRepresentation User);