namespace KeycloakAuthIntegration.Keycloak.Saga.Interfaces;

public interface ISagaHandler<in T> where T : class
{
    Task Handle(T dto, CancellationToken cancellationToken);
}