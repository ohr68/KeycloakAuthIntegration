using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Domain.Interfaces.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace KeycloakAuthIntegration.Messaging.Consumers;

public class UserSynchronizedConsumer(IUserSyncService userSyncService, ILogger<UserSynchronizedConsumer> logger) : IConsumer<UserSynchronized>
{
    public async Task Consume(ConsumeContext<UserSynchronized> context)
    {
        logger.LogInformation("Starting UserSynchronizedConsumer for {UserId}", context.Message.Id);
        
        var message = context.Message;
        
        var result = await userSyncService.SynchronizeUser(message, context.CancellationToken);
        
        logger.LogInformation("User {messageId} {result}.", message.Id, result ? "synchronized successfully" : "failed to synchronize");
    }
}