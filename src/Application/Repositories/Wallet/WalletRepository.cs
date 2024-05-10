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
        /// <param name="safeId">Bank id for get wallets of specific bank.</param>
        public async Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(Guid? safeId = null, int page = 1, int pageSize = 10,
            CancellationToken cancellationToken = default, TypeAdapterConfig? config = null)
        {
            var walletsQuery = context.Wallets.OrderBy(w => w.CreatedDateTime)
                .AsQueryable();
            if (safeId is not null)
                walletsQuery = walletsQuery.Where(b => b.SafeId == safeId);
            else
                walletsQuery = walletsQuery.Where(w => w.SafeId == null);
            return await walletsQuery.PaginatedListAsync<WalletEntity, TDestination>(page, pageSize, cancellationToken, config);
        }

        /// <summary>
        /// Find a specific wallet with seed.
        /// </summary>
        /// <param name="seed">Wallet seed value.</param>
        /// <param name="safeId">specific bank id.</param>
        /// <returns>Wallet model object.</returns>
        public async Task<WalletEntity?> FindBySeedAsync(string seed, Guid? safeId = null,
            CancellationToken cancellationToken = default)
        {
            var wallet = context.Wallets.Where(w => w.Seed == seed);
            #region Filter
            if (safeId is not null || safeId != Guid.Empty)
                wallet = wallet.Where(w => w.SafeId == safeId);
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
            try
            {
                await context.Wallets.AddAsync(wallet);
                if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                    return Result.Ok();
                return Result.Fail();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
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
                return Result.Ok();
            return Result.Fail();
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
                return Result.Ok();
            return Result.Fail();
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