﻿using System.Text.Json.Serialization;

namespace KeycloakAuthIntegration.Keycloak.Models.Requests;

public class AuthRequest
{
    [JsonPropertyName("grant_type")]
    public string? GrantType { get; set; }
    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }
    [JsonPropertyName("client_secret")]
    public string? ClientSecret { get; set; }
    [JsonPropertyName("username")]
    public string? Username { get; set; }
    [JsonPropertyName("password")]
    public string? Password { get; set; }
}