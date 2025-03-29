using FluentValidation;
using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Common.Messaging.Interfaces;
using KeycloakAuthIntegration.Domain.Entities;
using KeycloakAuthIntegration.Domain.Interfaces;
using KeycloakAuthIntegration.ORM.Context;
using KeycloakIntegration.Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KeycloakAuthIntegration.Application.CQRS.Users.CreateUser;

public class CreateUserCommandHandler(
    AuthDbContext context,
    IQueueService queueService,
    IPasswordHasher passwordHasher,
    IValidator<CreateUserCommand> createUserValidator,
    ILogger<CreateUserCommandHandler> logger) : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await createUserValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = request.Adapt<User>();

        context.Users.Add(user);
        user.Password = passwordHasher.HashPassword(user.Password!);

        var saveResult = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!saveResult)
            throw new BadRequestException("Houve uma falha ao inserir o usuário. Tente novamente.");

        logger.LogInformation("Sending created user to queue.");
        var userCreatedEvent = user.Adapt<UserCreated>();
        userCreatedEvent.Password = request.Password;

        await queueService.Publish(userCreatedEvent, cancellationToken);
        logger.LogInformation("User sent to queue successfully.");

        return user.Adapt<CreateUserResult>();
    }
}