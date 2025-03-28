using FluentValidation;

namespace KeycloakAuthIntegration.Application.CQRS.Auth.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotEmpty()
            .WithMessage("É obrigatório informar o token.");
    }
}