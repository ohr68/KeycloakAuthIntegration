using FluentValidation;
using KeycloakAuthIntegration.Domain.Entities;
using KeycloakAuthIntegration.ORM.Context;
using KeycloakIntegration.Common.Exceptions;
using Mapster;
using MediatR;

namespace KeycloakAuthIntegration.Application.CQRS.Users.CreateUser;

public class CreateUserCommandHandler(AuthDbContext context, IValidator<CreateUserCommand> createUserValidator) : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await createUserValidator.ValidateAsync(request, cancellationToken);
        
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var user = request.Adapt<User>();
        context.Users.Add(user);
        
        var saveResult = await context.SaveChangesAsync(cancellationToken) > 0;

        if (!saveResult)
            throw new BadRequestException("Houve uma falha ao inserir o usuário. Tente novamente.");
        
        //TODO: adicionar envio de dados para a fila
        
        return user.Adapt<CreateUserResult>();
    }
}