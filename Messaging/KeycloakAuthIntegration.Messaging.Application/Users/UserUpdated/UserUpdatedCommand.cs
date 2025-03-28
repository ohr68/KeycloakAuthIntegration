﻿using MediatR;

namespace KeycloakAuthIntegration.Messaging.Application.Users.UserUpdated;

public class UserUpdatedCommand : IRequest<UserUpdatedResult>
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}