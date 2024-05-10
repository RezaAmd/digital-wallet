using DigitalWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class BankConfiguration : IEntityTypeConfiguration<BankEntity>
    {
        public void Configure(EntityTypeBuilder<BankEntity> b)
        {
            b.ToTable("Banks");
            b.HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}