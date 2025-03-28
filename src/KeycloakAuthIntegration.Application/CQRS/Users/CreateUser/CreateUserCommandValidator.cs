﻿using FluentValidation;
using KeycloakAuthIntegration.Application.CustomValidators;

namespace KeycloakAuthIntegration.Application.CQRS.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.Username)
            .NotEmpty()
            .WithMessage("É obrigatório informar o nome do usuário.")
            .MaximumLength(100)
            .WithMessage("O tamanho máximo permitido para o nome de usuário é de 100 caracteres.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("É obrigatório informar o e-mail.")
            .EmailAddress()
            .WithMessage("Informe um e-mail válido.")
            .MaximumLength(150)
            .WithMessage("O tamanho máximo permitido para o e-mail é de 150 caracteres.");

        RuleFor(c => c.FirstName)
            .NotEmpty()
            .WithMessage("É obrigatório informar o primeiro nome.")
            .MaximumLength(100)
            .WithMessage("O tamanho máximo permitido para o primeiro nome é de 100 caracteres.");

        RuleFor(c => c.LastName)
            .NotEmpty()
            .WithMessage("É obrigatório informar o sobrenome.")
            .MaximumLength(100)
            .WithMessage("O tamanho máximo permitido para o sobrenome é de 100 caracteres.");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("É obrigatório informar a senha.")
            .SetValidator(new StrongPasswordValidator<CreateUserCommand>()!);
    }
}