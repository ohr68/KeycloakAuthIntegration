using FluentValidation;

namespace KeycloakAuthIntegration.Application.CQRS.Users.SynchronizeUser;

public class SynchronizeUserCommandValidator : AbstractValidator<SynchronizeUserCommand>
{
    public SynchronizeUserCommandValidator()
    {
        RuleFor(s => s.Id)
            .NotEmpty()
            .WithMessage("É obrigatório informar o Id do usuário.");

        RuleFor(s => s.Status)
            .IsInEnum()
            .WithMessage("Informe um status válido.");
    }
}