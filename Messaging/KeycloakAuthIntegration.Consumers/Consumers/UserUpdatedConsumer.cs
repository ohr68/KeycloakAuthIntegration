using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Common.Messaging.Interfaces;
using KeycloakAuthIntegration.Messaging.Application.Users.UserUpdated;
using Mapster;
using MassTransit;
using MediatR;

namespace KeycloakAuthIntegration.Consumers.Consumers;

public class UserUpdatedConsumer(IMediator mediator, IQueueService queueService, ILogger<UserUpdatedConsumer> logger) : IConsumer<UserUpdated>
{
    public async Task Consume(ConsumeContext<UserUpdated> context)
    {
        logger.LogInformation("Starting UserCreatedConsumer for {UserId}", context.Message.Id);

        var message = context.Message;

        var userCreatedCommand = message.Adapt<UserUpdatedCommand>();

        _ = await mediator.Send(userCreatedCommand, context.CancellationToken);

        logger.LogInformation("Sending UserSynchronized to queue.");
        await queueService.Publish(message.Adapt<UserSynchronized>(), context.CancellationToken);
        logger.LogInformation("UserSynchronized sent to queue.");
    }
}