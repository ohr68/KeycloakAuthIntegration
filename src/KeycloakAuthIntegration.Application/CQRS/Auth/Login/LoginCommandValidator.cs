using FluentValidation;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(a => a.Username)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome do usuário.")
            .MaximumLength(100)
            .WithMessage("O nome de usuário deve possuir no máximo 100 caracteres.");

        RuleFor(a => a.Password)
            .NotEmpty()
            .WithMessage("É obrigatório informar a senha.");
    }
}