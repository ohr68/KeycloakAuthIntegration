using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
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
                EmailVerified = true,
                Credentials = string.IsNullOrEmpty(u.Password)
                    ? null
                    : new[]
                    {
                        new CredentialRepresentation
                        {
                            Type = CredentialType.Password,
                            Value = u.Password,
                            Temporary = false
                        }
                    }
            });

        config.NewConfig<UserSync, UserUpdatedResult>();
        config.NewConfig<UserUpdatedResult, UserSynchronized>();
    }
}