using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace  DigitalWallet.Application.Interfaces.Context
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Permission> Permissions { get; set; }

        DbSet<Bank> Banks { get; set; }
        DbSet<Wallet> Wallets { get; set; }
        DbSet<Deposit> Deposits { get; set; }
        DbSet<Transfer> Transfers { get; set; }
    }
}