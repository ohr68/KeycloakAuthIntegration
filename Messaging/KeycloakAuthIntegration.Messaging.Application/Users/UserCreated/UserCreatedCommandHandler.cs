using System.Text.Json;
using FluentValidation;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models.Dtos;
using KeycloakAuthIntegration.Keycloak.Saga.Interfaces.CreateUser;
using KeycloakAuthIntegration.Messaging.Domain.Entities;
using KeycloakAuthIntegration.Messaging.ORM.Context;
using KeycloakIntegration.Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserCreated;

public class UserCreatedCommandHandler(
    MessagingDbContext context,
    ICreateUserHandler createUserHandler,
    IValidator<UserCreatedCommand> validator
) : IRequestHandler<UserCreatedCommand, UserCreatedResult>
{
    public async Task<UserCreatedResult> Handle(UserCreatedCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var userSync = request.Adapt<UserSync>();
        context.UsersSync.Add(userSync);

        var saveResult = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!saveResult)
            throw new BadRequestException("Houve uma falha ao inserir o usuário. Tente novamente.");

        var userRepresentation = request.Adapt<CreateUserFlowDto>();

        await createUserHandler.Handle(userRepresentation, cancellationToken);

        userSync.Synchronized();
        context.Entry(userSync).State = EntityState.Modified;

        var updateResult = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!updateResult)
            throw new BadRequestException("Houve uma falha ao atualizar o usuário.");

        return userSync.Adapt<UserCreatedResult>();
    }
}