using KeycloakAuthIntegration.Messaging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeycloakAuthIntegration.Messaging.ORM.Mapping;

public class UserSyncConfiguration : IEntityTypeConfiguration<UserSync>
{
    public void Configure(EntityTypeBuilder<UserSync> builder)
    {
        builder.ToTable("UsersSync");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType("uniqueidentifier")
            .IsRequired();
        
        builder.Property(u => u.CreatedAt)
            .HasColumnType("datetime2(7)")
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnType("datetime2(7)");

        builder.Property(u => u.Status)
            .HasColumnType("int")
            .IsRequired();
    }
}