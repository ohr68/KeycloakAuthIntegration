namespace KeycloakAuthIntegration.Domain.Entities;

public class User : EntityBase
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public bool LoginAllowed { get; set; }

    public void Synchronized()
    {
        UpdatedAt = DateTime.UtcNow;
        LoginAllowed = true;
    }

    public bool CanLogin() => LoginAllowed;
}