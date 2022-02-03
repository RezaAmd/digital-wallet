using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class PermissionRoleConfiguration : IEntityTypeConfiguration<PermissionRole>
    {
        public void Configure(EntityTypeBuilder<PermissionRole> b)
        {
            b.ToTable("PermissionRoles");
            b.HasKey(w => new { w.PermissionId, w.RoleId });
        }
    }
}