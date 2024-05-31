using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Application.Interfaces.Context
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbSet<UserEntity> Users { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<PermissionEntity> Permissions { get; set; }

        DbSet<WalletEntity> Wallets { get; set; }
        DbSet<DepositEntity> Deposits { get; set; }
        DbSet<TransferEntity> Transfers { get; set; }
    }
}