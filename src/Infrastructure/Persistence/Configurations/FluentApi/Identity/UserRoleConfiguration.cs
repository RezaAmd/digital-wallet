using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> b)
        {
            b.ToTable("UserRoles");
            b.HasKey(w => new { w.UserId, w.RoleId });
        }
    }
}