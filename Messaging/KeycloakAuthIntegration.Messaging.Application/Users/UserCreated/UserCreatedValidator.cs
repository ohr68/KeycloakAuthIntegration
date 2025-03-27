using FluentValidation;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserCreated;

public class UserCreatedValidator : AbstractValidator<UserCreatedCommand>
{
    public UserCreatedValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .WithMessage("É obrigatório informar o id do usuário.");

        RuleFor(u => u.Username)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome de usuário.");
        
        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("É obrigatório informar o e-mail do usuário.")
            .EmailAddress()
            .WithMessage("Informe um e-mail válido.");

        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage("É obrigatório informar o primeiro nome do usuário.");

        RuleFor(u => u.LastName)
            .NotEmpty()
            .WithMessage("É obrigatório informar o sobrenome do usuário.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("É obrigatório informar a senha do usuário.");

        RuleFor(u => u.CreatedAt)
            .NotEmpty()
            .WithMessage("É obrigatório informar a data de criação do usuário.");
    }
}