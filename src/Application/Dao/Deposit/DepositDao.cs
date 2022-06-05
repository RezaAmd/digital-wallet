using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.Context;
using Application.Models;
using Domain.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dao
{
    public class DepositDao : IDepositDao
    {
        #region Initialize
        private readonly IDbContext context;
        public DepositDao(IDbContext _context)
        {
            context = _context;
        }
        #endregion

        /// <summary>
        /// Find deposit history by id.
        /// </summary>
        /// <param name="id">Deposit history id (GUID).</param>
        /// <returns>Deposit model object.</returns>
        public async Task<Deposit> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await context.Deposits.FindAsync(id, cancellationToken);
        }

        public async Task<Deposit> FindByTraceIdAsync(string traceId, CancellationToken cancellationToken = default)
        {
            return await context.Deposits.Where(d => d.TraceId == traceId).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Get deposit history by wallet id.
        /// </summary>
        /// <param name="walletId">Wallet id to fetch deposit history.</param>
        public async Task<PaginatedList<Deposit>> GetByWalletIdAsync(string walletId, int page = 1, int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await context.Deposits
                .Where(d => d.DestinationId == walletId)
                .PaginatedListAsync(page, pageSize, cancellationToken);
        }

        /// <summary>
        /// Create a new deposit history.
        /// </summary>
        /// <param name="deposit">New deposit model object.</param>
        public async Task<Result> CreateAsync(Deposit deposit, CancellationToken cancellationToken = default)
        {
            await context.Deposits.AddAsync(deposit, cancellationToken);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Update a deposit history.
        /// </summary>
        /// <param name="deposit">Edited deposit history model object.</param>
        public async Task<Result> UpdateAsync(Deposit deposit, CancellationToken cancellationToken = default)
        {
            context.Deposits.Update(deposit);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }
    }
}