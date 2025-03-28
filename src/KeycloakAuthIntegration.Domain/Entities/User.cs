using KeycloakAuthIntegration.Common.Messaging.Enums;

namespace KeycloakAuthIntegration.Domain.Entities;

public class User : EntityBase
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public bool LoginAllowed { get; set; }
    public SyncStatus Status { get; set; }

    public void CreationSynchronized()
    {
        Updated();
        LoginAllowed = true;
        Synchronized();
    }

    public void UpdateSynchronized()
    {
        Updated();
        Synchronized();
    }

    public void MustSynchronize()
    {
        Status = SyncStatus.Pending;
    }

    private void Synchronized()
    {
        Status = SyncStatus.Completed;
    }

    public bool CanLogin() => LoginAllowed;
}