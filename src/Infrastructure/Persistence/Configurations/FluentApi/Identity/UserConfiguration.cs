using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> b)
        {
            b.ToTable("User", DatabaseSchemaDefaults.Dbo);

            // Email
            b.Property(u => u.Email)
                .IsRequired(false)
                .HasMaxLength(350);

            // IsEmailConfirmed
            b.Property(u => u.IsEmailConfirmed)
                .IsRequired()
                .HasDefaultValue(false);

            // PhoneNumber
            b.Property(e => e.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(15);

            // IsPhoneNumberConfirmed
            b.Property(u => u.IsPhoneNumberConfirmed)
                .IsRequired()
                .HasDefaultValue(false);

            // Password
            b.OwnsOne(u => u.Password, u =>
            {
                u.Property(p => p.Value)
                .HasColumnName("Password")
                .HasMaxLength(256)
                .IsRequired(false)
                ;
            });

            // Fullname
            b.OwnsOne(u => u.Fullname, u =>
            {
                u.Property(fn => fn.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired(false);

                u.Property(fn => fn.Surname)
                .HasColumnName("Surname")
                .HasMaxLength(50)
                .IsRequired(false);
            });

            // JoinedDate
            b.Property(u => u.JoinedDate)
                .ValueGeneratedOnAdd()
                .IsRequired();

            // IsBanned
            b.Property(u => u.IsBanned)
                .IsRequired()
                .HasDefaultValue(false);

            #region Relations

            // UserRoles - (ManyToMany)
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            // Safes - (OneToMany)
            b.HasMany(u => u.Safes)
                .WithOne(s => s.Owner)
                .HasForeignKey(s => s.OwnerId);

            #endregion
        }
    }
}