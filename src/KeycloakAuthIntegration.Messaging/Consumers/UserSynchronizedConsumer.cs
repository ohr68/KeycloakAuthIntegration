using System.Text.Json;
using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Domain.Interfaces.Services;
using MassTransit;

namespace KeycloakAuthIntegration.Messaging.Consumers;

public class UserSynchronizedConsumer(IUserSyncService userSyncService) : IConsumer<UserSynchronized>
{
    public async Task Consume(ConsumeContext<UserSynchronized> context)
    {
        Console.WriteLine("Starting UserSynchronizedConsumer for {0}", context.Message.CorrelationId);
        
        var message = context.Message;
        Console.WriteLine(JsonSerializer.Serialize(message));
        
        var result = await userSyncService.SynchronizeUser(message, context.CancellationToken);
        
        Console.WriteLine("User {0} {1}.", message.Id, result ? "synchronized successfully" : "failed to synchronize");
    }
}