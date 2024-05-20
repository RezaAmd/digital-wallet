using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class WalletConfiguration : IEntityTypeConfiguration<WalletEntity>
    {
        public void Configure(EntityTypeBuilder<WalletEntity> builder)
        {
            builder.ToTable("Wallet", DatabaseSchemaDefaults.Dbo);

            // Seed
            builder.Property(w => w.Seed)
                .IsRequired()
                .HasMaxLength(200);

            // Identifier
            builder.Property(w => w.Identifier)
                .IsRequired()
                .HasMaxLength(50);

            // CreatedDateTime
            builder.Property(w => w.CreatedDateTime)
                .IsRequired();

            // OwnerId
            builder.Property(w => w.OwnerId)
                .IsRequired();

            // SafeId
            builder.Property(w => w.SafeId)
                .IsRequired(false);

            #region Relations

            // Owner
            builder.HasOne(w => w.Owner)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.OwnerId);

            // Safe
            builder.HasOne(w => w.Safe)
                .WithMany(s => s.Wallets)
                .HasForeignKey(w => w.SafeId);

            // Deposits
            builder.HasMany(w => w.Deposits)
                .WithOne(w => w.Wallet)
                .HasForeignKey(d => d.DestinationId);

            #endregion
        }
    }
}