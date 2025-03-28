﻿using MediatR;

namespace KeycloakAuthIntegration.Application.CQRS.Users.CreateUser;

public class CreateUserCommand : IRequest<CreateUserResult>
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
}