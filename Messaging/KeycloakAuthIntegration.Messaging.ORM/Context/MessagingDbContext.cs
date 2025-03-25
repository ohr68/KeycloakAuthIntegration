using KeycloakAuthIntegration.Messaging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace KeycloakAuthIntegration.Messaging.ORM.Context;

public class MessagingDbContext(DbContextOptions<MessagingDbContext> options) : DbContext(options)
{
    public DbSet<UserSync> UsersSync { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrmLayer).Assembly);
    }
}