using FluentValidation;
using KeycloakAuthIntegration.Domain.Interfaces;
using KeycloakAuthIntegration.Keycloak.Configuration;
using KeycloakAuthIntegration.Keycloak.Constants;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.Models.Requests;
using KeycloakAuthIntegration.ORM.Context;
using KeycloakIntegration.Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.Login;

public class LoginCommandHandler(
    AuthDbContext context,
    IAuthRequestHandler authRequestHandler,
    IAuthService authService,
    IPasswordHasher passwordHasher,
    IOptions<KeycloakUserOptions> keycloakDefaultUser,
    IValidator<LoginCommand> validator) : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken)
                   ?? throw new NotFoundException($"Usuário '{request.Username}' não encontrado.");

        if (request.Username != keycloakDefaultUser.Value.Username && !passwordHasher.VerifyPassword(user.Password!, request.Password!))
            throw new BadRequestException("Senha inválida.");

        if (!user.CanLogin())
            throw new ForbiddenException($"Login não permitido para o usuário '{request.Username}'.\nTente novamente mais tarde.");

        var (grantType, clientId, clientSecret) = authRequestHandler.GetAuthRequestData(GrantType.Password);
        request.SetAuthData(grantType, clientId, clientSecret);

        var authResponse = await authService.AuthenticateAsync(request.Adapt<AuthRequest>(), cancellationToken);

        return authResponse.Adapt<LoginResult>();
    }
}