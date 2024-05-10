using  DigitalWallet.Application.Interfaces.Context;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalWallet.Infrastructure.Persistence.Context
{
    public class IdentityDbContext : DbContext, IDbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RoleEntity> Roles { get; set; }
        public virtual DbSet<UserRoleEntity> UserRoles { get; set; }
        #region Permissions
        public virtual DbSet<PermissionEntity> Permissions { get; set; }
        public virtual DbSet<UserPermissionEntity> UserPermissions { get; set; }
        public virtual DbSet<PermissionRoleEntity> PermissionRoles { get; set; }
        #endregion
        public virtual DbSet<BankEntity> Banks { get; set; }
        public virtual DbSet<WalletEntity> Wallets { get; set; }
        public virtual DbSet<DepositEntity> Deposits { get; set; }
        public virtual DbSet<TransferEntity> Transfers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}