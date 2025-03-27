using KeycloakAuthIntegration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeycloakAuthIntegration.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(u => u.Username)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(u => u.Password)
            .HasColumnType("varchar(max)")
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasColumnType("datetime2(7)")
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnType("datetime2(7)");

        builder.Property(u => u.LoginAllowed)
            .HasColumnType("bit")
            .IsRequired();

        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}