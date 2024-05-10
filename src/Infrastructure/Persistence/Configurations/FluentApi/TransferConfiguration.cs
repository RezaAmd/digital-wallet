using DigitalWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class TransferConfiguration : IEntityTypeConfiguration<TransferEntity>
    {
        public void Configure(EntityTypeBuilder<TransferEntity> b)
        {
            b.ToTable("Transfers");
            b.HasIndex(d => d.Id)
                .IsUnique();
            b.Property(d => d.DestinationId).IsRequired();
            b.OwnsOne(d => d.Amount);
        }
    }
}