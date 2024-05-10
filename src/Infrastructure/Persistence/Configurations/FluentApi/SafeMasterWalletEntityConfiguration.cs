using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class SafeMasterWalletEntityConfiguration : IEntityTypeConfiguration<SafeMasterWalletEntity>
    {
        public void Configure(EntityTypeBuilder<SafeMasterWalletEntity> builder)
        {
            builder.ToTable("SafeMasterWallet", DatabaseSchemaDefaults.Dbo);

            // WalletId
            builder.Property(e => e.WalletId)
                .IsRequired();

            // SafeId
            builder.Property(e => e.SafeId)
                .IsRequired();

            #region Relationship

            // Wallet (FK)
            builder.HasOne(smw => smw.Wallet)
                .WithOne()
                .HasForeignKey<SafeMasterWalletEntity>(smw => smw.WalletId);

            // Safe (FK)
            builder.HasOne(smw => smw.Safe)
                .WithOne()
                .HasForeignKey<SafeMasterWalletEntity>(smw => smw.SafeId);

            #endregion
        }
    }
}