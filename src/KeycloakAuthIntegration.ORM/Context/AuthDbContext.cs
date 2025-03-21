using Microsoft.EntityFrameworkCore;

namespace KeycloakAuthIntegration.ORM.Context;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrmLayer).Assembly);
    }
}