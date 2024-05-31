using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> b)
        {
            b.ToTable("UserRole", DatabaseSchemaDefaults.Dbo);

            // AssignedDateTime
            b.Property(ur => ur.CreatedOn);

            // Type
            b.Property(ur => ur.Type);

            // RelatedToId
            b.Property(ur => ur.RelatedToId)
                .IsRequired(false);

            #region Relations

            // User
            b.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            // Role
            b.HasOne(ur => ur.Role)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            #endregion
        }
    }
}