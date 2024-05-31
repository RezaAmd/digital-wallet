using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class PermissionRoleConfiguration : IEntityTypeConfiguration<PermissionRoleEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionRoleEntity> b)
        {
            b.ToTable("PermissionRole", DatabaseSchemaDefaults.Dbo);

            //Id
            b.Property(pr => pr.Id);

            // CreatedOn
            b.Property(pr => pr.CreatedOn);

            // RoleId
            b.Property(pr => pr.CreatedOn);
            
            // PermissionId
            b.Property(pr => pr.CreatedOn);

            #region Relations

            // Role
            b.HasOne(pr => pr.Role)
                .WithMany(r => r.PermissionRoles)
                .HasForeignKey(pr => pr.RoleId);

            // Permission
            b.HasOne(pr => pr.Permission)
                .WithMany(p => p.PermissionRoles)
                .HasForeignKey(pr => pr.PermissionId);

            #endregion
        }
    }
}