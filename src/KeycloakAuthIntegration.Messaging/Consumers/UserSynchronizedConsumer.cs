using System.Text.Json;
using KeycloakAuthIntegration.Common.Messaging.Messaging.Users;
using KeycloakAuthIntegration.Messaging.Commands;
using MassTransit;

namespace KeycloakAuthIntegration.Messaging.Consumers;

public class UserSynchronizedConsumer : IConsumer<UserSynchronized>
{
    public async Task Consume(ConsumeContext<UserSynchronized> context)
    {
        Console.WriteLine("Starting UserSynchronizedConsumer for {0}", context.Message.CorrelationId);
        
        var message = context.Message;
        Console.WriteLine(JsonSerializer.Serialize(message));
        
        var userSynchronizedCommand = new UserSynchronizedCommand();
        //
        // var result = await mediator.Send(userCreatedCommand, context.CancellationToken);
        
        //Console.WriteLine(JsonSerializer.Serialize(result));
    }
}