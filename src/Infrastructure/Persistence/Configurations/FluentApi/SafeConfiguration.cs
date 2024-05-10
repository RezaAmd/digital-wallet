using DigitalWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class SafeConfiguration : IEntityTypeConfiguration<SafeEntity>
    {
        public void Configure(EntityTypeBuilder<SafeEntity> b)
        {
            b.ToTable("Safe");
            b.HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}