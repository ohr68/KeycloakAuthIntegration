using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Common.Messaging.Interfaces;
using KeycloakAuthIntegration.Messaging.Application.Users.UserUpdated;
using Mapster;
using MassTransit;
using MediatR;

namespace KeycloakAuthIntegration.Consumers.Consumers;

public class UserUpdatedConsumer(IMediator mediator, IQueueService queueService) : IConsumer<UserUpdated>
{
    public async Task Consume(ConsumeContext<UserUpdated> context)
    {
        Console.WriteLine("Starting UserCreatedConsumer for {0}", context.Message.CorrelationId);

        var message = context.Message;

        var userCreatedCommand = message.Adapt<UserUpdatedCommand>();

        _ = await mediator.Send(userCreatedCommand, context.CancellationToken);

        Console.WriteLine("Sending UserSynchronized to queue.");
        await queueService.Publish(message.Adapt<UserSynchronized>(), context.CancellationToken);
        Console.WriteLine("UserSynchronized sent to queue.");
    }
}