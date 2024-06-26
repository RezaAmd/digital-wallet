﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class DepositConfiguration : IEntityTypeConfiguration<DepositEntity>
    {
        public void Configure(EntityTypeBuilder<DepositEntity> b)
        {
            b.ToTable("Deposit", DatabaseSchemaDefaults.Dbo);

            // Id
            b.HasKey(x => x.Id);

            // TraceId
            b.Property(w => w.TraceId)
                .HasMaxLength(200);
            b.HasIndex(d => d.TraceId)
                .IsUnique();

            // Authority
            b.Property(w => w.Authority)
                .HasMaxLength(500);

            // RefId
            b.Property(w => w.RefId)
                .HasMaxLength(100);

            // Amount
            b.OwnsOne(s => s.Amount, d =>
            {
                d.Property(p => p.Value)
                .HasColumnName("Amount")
                .IsRequired();
            });

            // Callback
            b.Property(w => w.Callback)
                .HasMaxLength(500);

            // DateTime
            b.Property(w => w.CreatedOn)
                .IsRequired();

            // DestinationId - Wallet
            b.Property(d => d.DestinationId)
                .IsRequired();

            #region Relations

            // Wallet (FK)
            b.HasOne(d => d.Wallet)
                .WithMany(w => w.Deposits)
                .HasForeignKey(d => d.DestinationId);

            #endregion
        }
    }
}