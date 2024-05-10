

using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermissionEntity>
    {
        public void Configure(EntityTypeBuilder<UserPermissionEntity> b)
        {
            b.ToTable("UserPermissions");

            b.HasKey(w => new { w.UserId, w.PermissionId });
        }
    }
}