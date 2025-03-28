using System.Text.Json;
using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Common.Messaging.Interfaces;
using KeycloakAuthIntegration.Messaging.Application.Users.UserCreated;
using Mapster;
using MassTransit;
using MediatR;

namespace KeycloakAuthIntegration.Consumers.Consumers;

public class UserCreatedConsumer(IMediator mediator, IQueueService queueService) : IConsumer<UserCreated>
{
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        Console.WriteLine("Starting UserCreatedConsumer for {0}", context.Message.CorrelationId);
        
        var message = context.Message;
        
        var userCreatedCommand = message.Adapt<UserCreatedCommand>();
      
        _ = await mediator.Send(userCreatedCommand, context.CancellationToken);
        
        Console.WriteLine("Sending UserSynchronized to queue.");
        await queueService.Publish(message.Adapt<UserSynchronized>(), context.CancellationToken);
        Console.WriteLine("UserSynchronized sent to queue.");
    }
}