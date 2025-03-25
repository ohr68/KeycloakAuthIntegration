using KeycloakAuthIntegration.Messaging.Domain.Messaging;
using MassTransit;

namespace KeycloakAuthIntegration.Consumers.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        throw new NotImplementedException();
    }
}