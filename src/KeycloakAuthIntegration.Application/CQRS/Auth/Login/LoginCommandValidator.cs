using FluentValidation;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(l => l.Username)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome do usuário.")
            .MaximumLength(100)
            .WithMessage("O nome de usuário deve possuir no máximo 100 caracteres.");

        RuleFor(l => l.Password)
            .NotEmpty()
            .WithMessage("É obrigatório informar a senha.");
    }
}