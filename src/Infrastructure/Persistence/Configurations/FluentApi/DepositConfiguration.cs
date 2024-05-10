using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class DepositConfiguration : IEntityTypeConfiguration<DepositEntity>
    {
        public void Configure(EntityTypeBuilder<DepositEntity> b)
        {
            b.ToTable("Deposits");
            b.HasIndex(d => d.Id)
                .IsUnique();
            b.Property(d => d.DestinationId).IsRequired();
            //b.OwnsOne(typeof(Money), "Amount");
            b.OwnsOne(d => d.Amount);
        }
    }
}