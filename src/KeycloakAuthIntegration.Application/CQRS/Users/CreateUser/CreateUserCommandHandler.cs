using System.Text.Json;
using FluentValidation;
using KeycloakAuthIntegration.Common.Messaging.Interfaces;
using KeycloakAuthIntegration.Domain.Entities;
using KeycloakAuthIntegration.Domain.Interfaces;
using KeycloakAuthIntegration.ORM.Context;
using KeycloakIntegration.Common.Exceptions;
using KeycloakIntegration.Common.Messaging.Users;
using Mapster;
using MediatR;
using Serilog;

namespace KeycloakAuthIntegration.Application.CQRS.Users.CreateUser;

public class CreateUserCommandHandler(
    AuthDbContext context,
    IQueueService queueService,
    IPasswordHasher passwordHasher,
    IValidator<CreateUserCommand> createUserValidator) : IRequestHandler<CreateUserCommand, CreateUserResult>
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

        Log.Information("Enviando usuário cadastrado para fila.");
        await queueService.Publish(user.Adapt<UserCreated>(), cancellationToken);
        Log.Information("Usuário enviado para fila com sucesso.");

        Console.WriteLine(JsonSerializer.Serialize(user));
        var result = user.Adapt<CreateUserResult>();
        Console.WriteLine(JsonSerializer.Serialize(result));
        
        return result;
    }
}