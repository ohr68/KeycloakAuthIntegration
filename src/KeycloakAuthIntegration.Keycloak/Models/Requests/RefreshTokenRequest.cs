﻿using System.Text.Json.Serialization;

namespace KeycloakAuthIntegration.Keycloak.Models.Requests;

public class RefreshTokenRequest
{
    [JsonPropertyName("grant_type")]
    public string? GrantType { get; set; }
    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }
    [JsonPropertyName("client_secret")]
    public string? ClientSecret { get; set; }
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}