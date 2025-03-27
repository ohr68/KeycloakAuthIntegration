namespace KeycloakAuthIntegration.Keycloak.Saga.CreateUser;

public enum CreateUserSteps
{
    CreateUser,
    GetCreatedUser,
    GetClientRole,
    AssignClientRole,
    GetRealmRole,
    AssignRealmRole
}