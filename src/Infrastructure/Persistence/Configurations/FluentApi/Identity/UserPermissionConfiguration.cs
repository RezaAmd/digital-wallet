using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermissionEntity>
    {
        public void Configure(EntityTypeBuilder<UserPermissionEntity> b)
        {
            b.ToTable("UserPermission", DatabaseSchemaDefaults.Dbo);

            // Id
            b.HasKey(u => u.Id);

            // CreatedOn
            b.Property(up => up.CreatedOn);

            // Type
            b.Property(up => up.Type);

            // RelatedToID
            b.Property(up => up.RelatedToId)
                .IsRequired(false);

            // UserId
            b.Property(up => up.UserId);

            // PermissionId
            b.Property(up => up.PermissionId);
        }
    }
}