using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Common.Messaging.Enums;
using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Keycloak.Models.Dtos;
using KeycloakAuthIntegration.Messaging.Domain.Entities;
using Mapster;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserCreated;

public class UserCreatedProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Common.Messaging.Commands.Users.UserCreated, UserCreatedCommand>();
        config.NewConfig<UserCreatedCommand, UserSync>();
        config.NewConfig<UserCreatedCommand, CreateUserFlowDto>()
            .ConstructUsing(u => new CreateUserFlowDto(u.Id, new UserRepresentation
            {
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Enabled = true,
                EmailVerified = true,
                Attributes = new Dictionary<string, List<string>>
                {
                    { Attributes.CustomId, new List<string> { u.Id.ToString() } }
                },
                Credentials = new[]
                {
                    new CredentialRepresentation
                    {
                        Type = CredentialType.Password,
                        Value = u.Password,
                        Temporary = false
                    }
                }
            }));

        config.NewConfig<UserSync, UserCreatedResult>();
        config.NewConfig<UserCreatedResult, UserSynchronized>()
            .ConstructUsing(u => new UserSynchronized(u.Id, u.Status, UserSyncOperation.Created));
    }
}