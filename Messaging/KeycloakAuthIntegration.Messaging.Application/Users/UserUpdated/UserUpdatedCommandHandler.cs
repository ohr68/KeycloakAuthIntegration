using FluentValidation;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models;
using KeycloakAuthIntegration.Messaging.Domain.Entities;
using KeycloakAuthIntegration.Messaging.ORM.Context;
using KeycloakIntegration.Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserUpdated;

public class UserUpdatedCommandHandler(MessagingDbContext context, IUserService userService, IValidator<UserUpdatedCommand> validator) : IRequestHandler<UserUpdatedCommand, UserUpdatedResult>
{
    public async Task<UserUpdatedResult> Handle(UserUpdatedCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var userSync = request.Adapt<UserSync>();
        context.UsersSync.Add(userSync);

        var saveResult = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!saveResult)
            throw new BadRequestException("Houve uma falha ao inserir o usuário. Tente novamente.");
        
        var userRepresentation = request.Adapt<UserRepresentation>();

        await userService.UpdateUserAsync(request.Username!, userRepresentation, cancellationToken);

        userSync.Synchronized();
        context.Entry(userSync).State = EntityState.Modified;

        var updateResult = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!updateResult)
            throw new BadRequestException("Houve uma falha ao atualizar o usuário.");

        return userSync.Adapt<UserUpdatedResult>();
    }
}