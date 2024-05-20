using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi
{
    public class SafeConfiguration : IEntityTypeConfiguration<SafeEntity>
    {
        public void Configure(EntityTypeBuilder<SafeEntity> builder)
        {
            builder.ToTable("Safe", DatabaseSchemaDefaults.Dbo);

            // Name
            builder.Property(s => s.Name)
                .IsRequired();

            // ApiKey
            builder.Property(s => s.ApiKey)
                .IsRequired(false);
            builder.HasIndex(s => s.ApiKey)
                .IsUnique();

            // Password
            builder.OwnsOne(s => s.Password, s =>
            {
                s.Property(p => p.Value)
                .HasColumnName("Password")
                .HasMaxLength(256)
                .IsRequired(false)
                ;
            });

            // CreatedDateTime
            builder.Property(s => s.CreatedDateTime)
                .IsRequired();

            #region Relations

            // Owner - (OneToMany)
            builder.HasOne(s => s.Owner)
                .WithMany(u => u.Safes)
                .HasForeignKey(s => s.OwnerId);

            // Wallets - (ManyToOne)
            builder.HasMany(s => s.Wallets)
                .WithOne(p => p.Safe)
                .HasForeignKey(b => b.SafeId);

            #endregion
        }
    }
}