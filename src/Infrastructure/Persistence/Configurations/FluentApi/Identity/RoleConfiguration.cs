﻿using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.FluentApi.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> b)
        {
            b.ToTable("Roles");

            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            b.HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}