using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> b)
        {
            b.ToTable("Role", DatabaseSchemaDefaults.Dbo);

            // Id
            b.HasKey(r => r.Id);

            // Name
            b.Property(r => r.Name)
                .HasMaxLength(100);
            b.HasIndex(r => r.Name)
                .IsUnique();

            // CreatedDateTime
            b.Property(r => r.CreatedOn);

            // Title
            b.Property(r => r.Title)
                .IsRequired(false)
                .HasMaxLength(200);

            // Description
            b.Property(r => r.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            #region Relations

            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            #endregion
        }
    }
}