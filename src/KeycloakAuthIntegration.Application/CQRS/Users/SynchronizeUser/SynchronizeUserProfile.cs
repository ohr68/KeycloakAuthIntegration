using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using Mapster;

namespace KeycloakAuthIntegration.Application.CQRS.Users.SynchronizeUser;

public class SynchronizeUserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserSynchronized, SynchronizeUserCommand>();
    }
}