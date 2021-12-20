using Application.Interfaces;
using Application.Interfaces.Context;
using Application.Models;
using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WalletService : IWalletService
    {
        #region Dependency Injection
        private readonly IDbContext context;
        public WalletService(IDbContext _context)
        {
            context = _context;
        }
        #endregion

        /// <summary>
        /// Find wallet by id.
        /// </summary>
        /// <param name="id">Wallet id</param>
        /// <returns>Wallet model object.</returns>
        public async Task<Wallet> FindByIdAsync(string id)
        {
            return await context.Wallets.FindAsync(id);
        }

        /// <summary>
        /// Create a new wallet.
        /// </summary>
        /// <param name="wallet">New wallet model object.</param>
        public async Task<Result> CreateAsync(Wallet wallet, CancellationToken cancellationToken = default)
        {
            await context.Wallets.AddAsync(wallet);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Update wallet object.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        public async Task<Result> UpdateAsync(Wallet wallet, CancellationToken cancellationToken = default)
        {
            context.Wallets.Update(wallet);
            if(Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Delete a wallet.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        public async Task<Result> DeleteAsync(Wallet wallet, CancellationToken cancellationToken = default)
        {
            context.Wallets.Remove(wallet);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }
    }
}