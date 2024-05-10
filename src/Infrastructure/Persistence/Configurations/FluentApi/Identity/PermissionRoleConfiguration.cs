using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class PermissionRoleConfiguration : IEntityTypeConfiguration<PermissionRoleEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionRoleEntity> b)
        {
            b.ToTable("PermissionRoles");
            b.HasKey(w => new { w.PermissionId, w.RoleId });
        }
    }
}