using FluentValidation;

namespace KeycloakAuthIntegration.Application.CQRS.Auth;

public class AuthCommandValidator : AbstractValidator<AuthCommand>
{
    public AuthCommandValidator()
    {
        RuleFor(a => a.Username)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome do usuário.");

        RuleFor(a => a.Password)
            .NotEmpty()
            .WithMessage("É obrigatório informar a senha.");
    }
}