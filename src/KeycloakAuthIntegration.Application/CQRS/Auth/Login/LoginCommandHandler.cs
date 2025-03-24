using FluentValidation;
using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakAuthIntegration.ORM.Context;
using KeycloakIntegration.Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.Login;

public class LoginCommandHandler(
    AuthDbContext context,
    IAuthRequestHandler authRequestHandler,
    IAuthService authService,
    IValidator<LoginCommand> validator) : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);
        
        if (user is null)
            throw new NotFoundException($"Usuário {request.Username} não encontrado.");

        var (grantType, clientId, clientSecret) = authRequestHandler.GetAuthRequestData(GrantType.Password);
        request.SetAuthData(grantType, clientId, clientSecret);

        var authResponse = await authService.AuthenticateAsync(request.Adapt<AuthRequest>(), cancellationToken);
        
        return authResponse.Adapt<LoginResult>();
    }
}