using FluentValidation;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserUpdated;

public class UserUpdatedCommandValidator : AbstractValidator<UserUpdatedCommand>
{
    public UserUpdatedCommandValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome de usuário.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("É obrigatório informar o e-mail.")
            .EmailAddress()
            .WithMessage("Informe um e-mail válido.");

        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage("É obrigatório informar o primeiro nome.");

        RuleFor(u => u.LastName)
            .NotEmpty()
            .WithMessage("É obrigatório informar o sobrenome.");
    }
}