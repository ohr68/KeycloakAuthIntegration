using System.Text.Json.Serialization;

namespace KeycloakAuthIntegration.Keycloak.Models;

public class UserProfileMetadata
{
    [JsonPropertyName("attributes")]
    public UserProfileAttributeMetadata[]? Attributes { get; set; }
    [JsonPropertyName("groups")]
    public UserProfileAttributeGroupMetadata[]? Groups { get; set; }
}