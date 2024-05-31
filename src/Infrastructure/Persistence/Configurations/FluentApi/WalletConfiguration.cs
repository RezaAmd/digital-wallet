using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class WalletConfiguration : IEntityTypeConfiguration<WalletEntity>
    {
        public void Configure(EntityTypeBuilder<WalletEntity> builder)
        {
            builder.ToTable("Wallet", DatabaseSchemaDefaults.Dbo);

            // Seed
            builder.Property(b => b.Seed)
                .IsRequired()
                .HasMaxLength(200);

            // Identifier
            builder.Property(b => b.Identifier)
                .IsRequired()
                .HasMaxLength(50);

            // ApiKey
            builder.Property(b => b.ApiKey)
                .IsRequired(false);
            builder.HasIndex(b => b.ApiKey)
                .IsUnique();

            // Password
            builder.OwnsOne(b => b.Password, w =>
            {
                w.Property(p => p.Value)
                .HasColumnName("Password")
                .HasMaxLength(256)
                .IsRequired(false)
                ;
            });

            // CreatedDateTime
            builder.Property(b => b.CreatedOn)
                .IsRequired();

            // OwnerId
            builder.Property(b => b.OwnerId)
                .IsRequired();

            #region Relations

            // Owner
            builder.HasOne(w => w.Owner)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.OwnerId);

            // Deposits
            builder.HasMany(w => w.Deposits)
                .WithOne(w => w.Wallet)
                .HasForeignKey(d => d.DestinationId);

            #endregion
        }
    }
}