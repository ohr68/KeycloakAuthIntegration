﻿using KeycloakAuthIntegration.Messaging.Domain.Enums;

namespace KeycloakAuthIntegration.Messaging.Domain.Entities;

public class UserSync
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public SyncStatus Status { get; set; }
}