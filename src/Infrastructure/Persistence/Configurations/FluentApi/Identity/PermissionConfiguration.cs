using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> b)
        {
            b.ToTable("Permissions");
            b.HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}