using FluentValidation;

namespace KeycloakAuthIntegration.Application.CQRS.Users.SynchronizeUser;

public class SynchronizeUserCommandValidator : AbstractValidator<SynchronizeUserCommand>
{
    public SynchronizeUserCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .WithMessage("É obrigatório informar o Id do usuário.");

        RuleFor(u => u.Status)
            .IsInEnum()
            .WithMessage("Informe um status válido.");
    }
}