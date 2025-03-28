using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Domain.Entities;
using Mapster;

namespace KeycloakAuthIntegration.Application.CQRS.Users.UpdateUser;

public class UpdateUserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserCommand, User>();
        config.NewConfig<User, UserUpdated>();
    }
}