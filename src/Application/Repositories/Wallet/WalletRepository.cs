using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Interfaces.Context;
using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Application.Repositories.Wallet
{
    public class WalletRepository : IWalletRepository
    {
        #region Dependency Injection
        private readonly IDbContext context;

        public WalletRepository(IDbContext _context)
        {
            context = _context;
        }
        #endregion

        /// <summary>
        /// Get all wallets.
        /// </summary>
        /// <param name="bankId">Bank id for get wallets of specific bank.</param>
        public async Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(Guid? bankId = null, int page = 1, int pageSize = 10,
            CancellationToken cancellationToken = default, TypeAdapterConfig? config = null)
        {
            var walletsQuery = context.Wallets.OrderBy(w => w.CreatedDateTime)
                .AsQueryable();
            if (bankId is not null)
                walletsQuery = walletsQuery.Where(b => b.BankId == bankId);
            else
                walletsQuery = walletsQuery.Where(w => w.BankId == null);
            return await walletsQuery.PaginatedListAsync<WalletEntity, TDestination>(page, pageSize, cancellationToken, config);
        }

        /// <summary>
        /// Find a specific wallet with seed.
        /// </summary>
        /// <param name="seed">Wallet seed value.</param>
        /// <param name="bankId">specific bank id.</param>
        /// <returns>Wallet model object.</returns>
        public async Task<WalletEntity?> FindBySeedAsync(string seed, Guid? bankId = null,
            CancellationToken cancellationToken = default)
        {
            var wallet = context.Wallets.Where(w => w.Seed == seed);
            #region Filter
            if (bankId is not null || bankId != Guid.Empty)
                wallet = wallet.Where(w => w.BankId == bankId);
            #endregion
            return await wallet.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Find wallet by id.
        /// </summary>
        /// <param name="id">Wallet id</param>
        /// <returns>Wallet model object.</returns>
        public async Task<WalletEntity?> FindByIdAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            return await context.Wallets.FindAsync(id);
        }

        /// <summary>
        /// Create a new wallet.
        /// </summary>
        /// <param name="wallet">New wallet model object.</param>
        public async Task<Result> CreateAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default)
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
        public async Task<Result> UpdateAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default)
        {
            context.Wallets.Update(wallet);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Delete a wallet.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        public async Task<Result> DeleteAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default)
        {
            context.Wallets.Remove(wallet);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Get a specific wallet balance.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        /// <returns>Wallet balance.</returns>
        public async Task<double> GetBalanceAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default)
        {
            var lastTransfer = await context.Transfers
                .Where(w => w.OriginId == wallet.Id || w.DestinationId == wallet.Id)
                .OrderBy(t => t.CreatedDateTime)
                .FirstOrDefaultAsync(cancellationToken);
            if (lastTransfer != null)
                return lastTransfer.DestinationBalance;
            return 0;
        }

        /// <summary>
        /// Find two wallet by id.
        /// </summary>
        /// <param name="firstId">First wallet id.</param>
        /// <param name="secondId">Second wallet id.</param>
        /// <returns>Return first and second wallet info.</returns>
        public async Task<(WalletEntity? first, WalletEntity? second)> GetTwoWalletByIdAsync(Guid firstId, Guid secondId,
            CancellationToken cancellationToken = default)
        {
            var wallets = await context.Wallets
                .Where(w => w.Id == firstId || w.Id == secondId).ToListAsync(cancellationToken);
            return (wallets.Where(w => w.Id == firstId).FirstOrDefault(),
                wallets.Where(w => w.Id == secondId).FirstOrDefault());
        }
    }
}