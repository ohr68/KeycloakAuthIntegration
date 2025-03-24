using KeycloakAuthIntegration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KeycloakAuthIntegration.ORM.Context;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrmLayer).Assembly);
    }
}