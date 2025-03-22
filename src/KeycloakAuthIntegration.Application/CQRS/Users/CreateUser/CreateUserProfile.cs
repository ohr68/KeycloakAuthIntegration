using KeycloakAuthIntegration.Domain.Entities;
using Mapster;

namespace KeycloakAuthIntegration.Application.CQRS.Users.CreateUser;

public class CreateUserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserCommand, User>();
        config.NewConfig<User, CreateUserResult>();
    }
}