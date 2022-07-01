using  DigitalWallet.Application.Extentions;
using  DigitalWallet.Application.Interfaces.Context;
using  DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace  DigitalWallet.Application.Repositories
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
        public async Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(string bankId = null, int page = 1, int pageSize = 10,
            CancellationToken cancellationToken = new CancellationToken(), TypeAdapterConfig config = default)
        {
            var walletsQuery = context.Wallets.OrderBy(w => w.CreatedDateTime)
                .AsQueryable();
            if (!string.IsNullOrEmpty(bankId))
                walletsQuery = walletsQuery.Where(b => b.BankId == bankId);
            else
                walletsQuery = walletsQuery.Where(w => w.BankId == null);
            return await walletsQuery.PaginatedListAsync<Wallet, TDestination>(page, pageSize, cancellationToken, config);
        }

        /// <summary>
        /// Find a specific wallet with seed.
        /// </summary>
        /// <param name="seed">Wallet seed value.</param>
        /// <param name="bankId">specific bank id.</param>
        /// <returns>Wallet model object.</returns>
        public async Task<Wallet> FindBySeedAsync(string seed, string bankId = null, CancellationToken cancellationToken = default)
        {
            var wallet = context.Wallets.Where(w => w.Seed == seed);
            #region Filter
            if (!string.IsNullOrEmpty(bankId))
                wallet = wallet.Where(w => w.BankId == bankId);
            #endregion
            return await wallet.FirstOrDefaultAsync(cancellationToken);
        }

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
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
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

        /// <summary>
        /// Get a specific wallet balance.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        /// <returns>Wallet balance.</returns>
        public async Task<double> GetBalanceAsync(Wallet wallet, CancellationToken cancellationToken = default)
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
        public async Task<(Wallet? first, Wallet? second)> GetTwoWalletByIdAsync(string firstId, string secondId, CancellationToken cancellationToken = new())
        {
            var wallets = await context.Wallets
                .Where(w => w.Id == firstId || w.Id == secondId).ToListAsync(cancellationToken);
            return (wallets.Where(w => w.Id == firstId).FirstOrDefault(),
                wallets.Where(w => w.Id == secondId).FirstOrDefault());
        }
    }
}