using FluentValidation;
using KeycloakAuthIntegration.ORM.Context;
using KeycloakIntegration.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KeycloakAuthIntegration.Application.CQRS.Users.SynchronizeUser;

public class SynchronizeUserCommandHandler(AuthDbContext context, IValidator<SynchronizeUserCommand> validator) : IRequestHandler<SynchronizeUserCommand, SynchronizeUserResult>
{
    public async Task<SynchronizeUserResult> Handle(SynchronizeUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await context.Users.SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"Usuário {request.Id} não encontrado.");
        
        user.Synchronized();
        context.Entry(user).State = EntityState.Modified;

        var rowsAffected = await context.SaveChangesAsync(cancellationToken) > 0;
        
        return new SynchronizeUserResult(rowsAffected);
    }
}