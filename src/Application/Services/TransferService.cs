using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.Context;
using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TransferService : ITransferService
    {
        #region Constructor
        private readonly IDbContext context;
        public TransferService(IDbContext _context)
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
            var transfers = context.Transfers.AsNoTracking();

            #region fillters
            if (!string.IsNullOrEmpty(walletId))
                transfers = transfers.Where(t => t.OriginId == walletId || t.DestinationId == walletId);
            if (startDate != default)
                transfers = transfers.Where(t => t.DateTime >= startDate);
            if (endDate != default)
                transfers = transfers.Where(t => t.DateTime <= endDate);
            #endregion

            return await transfers.PaginatedListAsync(page, pageSize, cancellationToken);
        }

        public async Task<double> GetBalanceAsync(string walletId, CancellationToken cancellationToken = default)
        {
            var transfer = await context.Transfers
                .Where(t => t.OriginId == walletId || t.DestinationId == walletId)
                .OrderBy(t => t.DateTime)
                .LastOrDefaultAsync(cancellationToken);
            if (transfer != null)
                return transfer.Balance;
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