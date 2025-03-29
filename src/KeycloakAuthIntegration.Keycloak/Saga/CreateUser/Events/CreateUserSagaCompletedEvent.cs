using MediatR;

namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser.Events;

public class CreateUserSagaCompletedEvent(string userId) : INotification
{
    public string UserId { get; set; } = userId;
}