using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> b)
        {
            b.ToTable("Permission", DatabaseSchemaDefaults.Dbo);

            // Id
            b.Property(p => p.Id);

            // Name
            b.Property(p => p.Name)
                .HasMaxLength(128);
            b.HasIndex(p => p.Name)
                .IsUnique();

            // Title
            b.Property(p => p.Title)
                .HasMaxLength(128)
                .IsRequired(false);

            // Description
            b.Property(p => p.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            // CreatedOn
            b.Property(p => p.CreatedOn)
                .HasMaxLength(128);

            #region Relations

            b.HasMany(p => p.PermissionRoles)
                .WithOne(pr => pr.Permission)
                .HasForeignKey(pr => pr.PermissionId);

            #endregion
        }
    }
}