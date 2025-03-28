using FluentValidation;
using KeycloakAuthIntegration.Common.Messaging.Commands.Users;
using KeycloakAuthIntegration.Common.Messaging.Interfaces;
using KeycloakAuthIntegration.ORM.Context;
using KeycloakIntegration.Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KeycloakAuthIntegration.Application.CQRS.Users.UpdateUser;

public class UpdateUserCommandHandler(AuthDbContext context, IQueueService queueService, IValidator<UpdateUserCommand> validator) :  IRequestHandler<UpdateUserCommand, UpdateUserResult>
{
    public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var user = await context.Users.SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                   ?? throw new NotFoundException($"Usuário {request.Id} não encontrado.");

        request.Adapt(user);
        
        user.Updated();
        context.Entry(user).State = EntityState.Modified;
        
        var rowsAffected = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!rowsAffected)
            throw new BadRequestException("Houve uma falha ao alterar o usuário.");
        
        Console.WriteLine("Enviando usuário alterado para fila.");
        var userUpdated = user.Adapt<UserUpdated>();
        userUpdated.Password = request.Password;
        
        await queueService.Publish(userUpdated, cancellationToken);
        Console.WriteLine("Usuário enviado para fila com sucesso.");
        
        return new UpdateUserResult(rowsAffected);
    }
}