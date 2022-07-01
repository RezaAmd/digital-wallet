using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.FluentApi
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> b)
        {
            b.ToTable("Transfers");
            b.HasIndex(d => d.Id)
                .IsUnique();
            b.Property(d => d.DestinationId).IsRequired();
            b.OwnsOne(d => d.Amount);
        }
    }
}