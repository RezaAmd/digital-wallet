using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class TransferConfiguration : IEntityTypeConfiguration<TransferEntity>
    {
        public void Configure(EntityTypeBuilder<TransferEntity> b)
        {
            b.ToTable("Transfers", DatabaseSchemaDefaults.Dbo);

            // Identify
            b.Property(t => t.Identify)
                .IsRequired();
            b.HasIndex(t => t.Identify)
                .IsUnique();

            // Amount
            b.OwnsOne(t => t.Amount, t => {
                t.Property(a => a.Value)
                .HasColumnName("Amount")
                .IsRequired();
            });

            // OriginType
            b.Property(a => a.OriginType);

            // OriginId - (FK)
            b.Property(t => t.OriginId)
                .IsRequired(false);

            // OriginBalance - Wallet
            b.Property(t => t.OriginBalance)
                .IsRequired(false);

            // DestinationId
            b.Property(t => t.DestinationId)
                .IsRequired();

            // DestinationBalance
            b.Property(t => t.DestinationBalance)
                .IsRequired();

            // CreatedDateTime
            b.Property(t => t.CreatedDateTime)
                .IsRequired();

            // State
            b.Property(t => t.State)
                .IsRequired();

            // Description
            b.Property(t => t.Description)
                .IsRequired(false);

            #region Relations

            // Destination - (OneToMany)
            b.HasOne(t => t.Destination)
                .WithMany()
                .HasForeignKey(t => t.DestinationId);

            #endregion
        }
    }
}