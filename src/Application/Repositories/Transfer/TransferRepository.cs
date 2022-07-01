using  DigitalWallet.Application.Extentions;
using  DigitalWallet.Application.Interfaces.Context;
using  DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace  DigitalWallet.Application.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        #region Constructor
        private readonly IDbContext context;

        public TransferRepository(IDbContext _context)
        {
            context = _context;
        }
        #endregion

        /// <summary>
        /// Find a transfer history by id.
        /// </summary>
        /// <param name="id">Transfer history id.</param>
        /// <returns>Transfer model object.</returns>
        public async Task<Transfer> FindByIdAsync(string id)
        {
            return await context.Transfers.FindAsync(id);
        }

        /// <summary>
        /// Get latest transform history by wallet id.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        /// <returns>Transfer model object.</returns>
        public async Task<(Transfer Transfer, double Balance)> GetLatestByWalletAsync(Wallet wallet, CancellationToken cancellationToken = new())
        {
            var transfer = await context.Transfers
                .OrderBy(t => t.CreatedDateTime)
                .Where(t => (t.OriginId == wallet.Id || t.DestinationId == wallet.Id)
                && t.State == TransferState.Success)
                .LastOrDefaultAsync(cancellationToken);
            double balance = 0;
            if (transfer != null)
            {
                if (!transfer.OriginBalance.HasValue && wallet.Id != transfer.DestinationId)
                    balance = 0;
                else
                    balance = transfer.OriginId == wallet.Id ?
                    transfer.OriginBalance.Value :
                    transfer.DestinationBalance;
            }
            return (transfer, balance);
        }

        public async Task<(Transfer first, Transfer second)> GetTwoLatestByWalletIdAsync(string firstId, string secondId, CancellationToken cancellationToken = new())
        {
            var transfers = await context.Transfers
                .OrderBy(t => t.CreatedDateTime)
                .Where(t => t.OriginId == firstId || t.DestinationId == firstId ||
                t.OriginId == secondId || t.DestinationId == secondId)
                .ToListAsync(cancellationToken);
            return (transfers.Where(t => t.OriginId == firstId || t.DestinationId == firstId).FirstOrDefault(),
                transfers.Where(t => t.OriginId == secondId || t.DestinationId == secondId).FirstOrDefault());
        }

        /// <summary>
        /// Get transfer history by wallet id
        /// </summary>
        /// <param name="walletId">Wallet id</param>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Page items count.</param>
        public async Task<PaginatedList<Transfer>> GetHistoryByWalletIdAsync(string walletId = default,
            DateTime startDate = default, DateTime endDate = default, int page = 1, int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var transfers = context.Transfers
                .OrderByDescending(x => x.CreatedDateTime)
                .AsNoTracking();

            #region fillters
            if (!string.IsNullOrEmpty(walletId))
                transfers = transfers.Where(t => t.OriginId == walletId || t.DestinationId == walletId);
            if (startDate != default)
                transfers = transfers.Where(t => t.CreatedDateTime >= startDate);
            if (endDate != default)
                transfers = transfers.Where(t => t.CreatedDateTime <= endDate);
            #endregion

            return await transfers.PaginatedListAsync(page, pageSize, cancellationToken);
        }

        /// <summary>
        /// Get wallet balance from latest transfer.
        /// </summary>
        /// <param name="walletId">Wallet id.</param>
        /// <returns>Wallet balance as double.</returns>
        public async Task<double> GetBalanceByIdAsync(Wallet wallet, CancellationToken cancellationToken = default)
        {
            var transfer = await context.Transfers
                .Where(t => t.OriginId == wallet.Id || t.DestinationId == wallet.Id)
                .OrderBy(t => t.CreatedDateTime)
                .LastOrDefaultAsync(cancellationToken);
            // Were any transaction found?
            if (transfer != null)
            {
                // Is it deposit?
                if (transfer.OriginType == TransferOriginType.Getway)
                {
                    if (wallet.Id == transfer.DestinationId)
                        return transfer.DestinationBalance;
                }
                // Is it transfer? (Increase/Decrease)
                else if (transfer.OriginType == TransferOriginType.Wallet)
                {
                    // Is it decrease?
                    if (wallet.Id == transfer.OriginId)
                        return transfer.OriginBalance.Value;
                    // Is it increase?
                    else if (wallet.Id == transfer.DestinationId)
                        return transfer.DestinationBalance;
                }
            }
            // Not found any transfer.
            return 0;
        }

        /// <summary>
        /// Create a new transfer history.
        /// </summary>
        /// <param name="transfer">New transfer history model object.</param>
        public async Task<Result> CreateAsync(Transfer transfer, CancellationToken cancellationToken = default)
        {
            await context.Transfers.AddAsync(transfer);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Update a Transfer history.
        /// </summary>
        /// <param name="transfer">Transfer model object.</param>
        public async Task<Result> UpdateAsync(Transfer transfer, CancellationToken cancellationToken = default)
        {
            context.Transfers.Update(transfer);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Delete a transfer history.
        /// </summary>
        /// <param name="transfer">Transfer model object.</param>
        public async Task<Result> DeleteAsync(Transfer transfer, CancellationToken cancellationToken = default)
        {
            context.Transfers.Remove(transfer);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }
    }
}