using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Common.Messaging.Enums;
using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Messaging.Domain.Entities;
using Mapster;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserUpdated;

public class UserUpdatedProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Common.Messaging.Commands.Users.UserCreated, UserUpdatedCommand>();
        config.NewConfig<UserUpdatedCommand, UserSync>();
        config.NewConfig<UserUpdatedCommand, UserRepresentation>()
            .ConstructUsing(u => new UserRepresentation
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Enabled = true,
                EmailVerified = true
            });

        config.NewConfig<UserSync, UserUpdatedResult>();
        config.NewConfig<UserUpdatedResult, UserSynchronized>()
            .ConstructUsing(u => new UserSynchronized(u.Id, u.Status, UserSyncOperation.Updated));
    }
}