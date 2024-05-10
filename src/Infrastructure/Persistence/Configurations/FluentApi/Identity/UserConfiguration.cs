using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> b)
        {
            b.ToTable("Users");

            b.Property(e => e.Name)
                .HasMaxLength(25);

            b.Property(e => e.Surname)
                .HasMaxLength(25);

            b.Property(e => e.PhoneNumber)
                .HasMaxLength(50);

            // Each User can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            // Remove Email
            //b.Ignore(u => u.Email)
            //    .Ignore(u => u.NormalizedEmail)
            //    .Ignore(u => u.EmailConfirmed);
        }
    }
}