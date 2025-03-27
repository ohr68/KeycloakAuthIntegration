using FluentValidation;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.Logout;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(l => l.UserId)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id do usuário.");
    }
}