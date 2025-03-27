using KeycloakAuthIntegration.Domain.Entities;
using KeycloakAuthIntegration.ORM.Context;

namespace KeycloakAuthIntegration.ORM.Initializers;

public class DbInitializer
{
    public static void SeedDatabase(AuthDbContext context)
    {
        if (context.Users.Any()) return;
        
        var admin = new User
        {
            Id = Guid.Parse("abddeaa3-31e9-4ab1-8376-1f24cd0bf339"),
            Username = "admin",
            FirstName = "Default",
            LastName = "Admin",
            Email = "admin@admin.com",
            Password = "admin",
            CreatedAt =  DateTime.UtcNow,
            LoginAllowed = true
        };
            
        context.Users.Add(admin);
        context.SaveChanges();
    }
}