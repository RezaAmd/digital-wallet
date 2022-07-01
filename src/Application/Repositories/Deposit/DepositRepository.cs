using Application.Extentions;
using Application.Interfaces.Context;
using Application.Models;
using Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class DepositRepository : IDepositRepository
    {
        #region Initialize
        private readonly IDbContext context;

        public DepositRepository(IDbContext _context)
        {
            context = _context;
        }
        #endregion

        /// <summary>
        /// Get all deposits history.
        /// </summary>
        /// <param name="page">Page number. (minimum: 1)</param>
        /// <param name="pageSize">Size of page items.</param>
        /// <param name="keyword">Keyword for search. (with traceId, refId or Authority)</param>
        /// <param name="includeWallet">Join to wallet table?</param>
        /// <param name="asNoTracking">As no tracking items?</param>
        /// <returns>Paginated list of deposit entity.</returns>
        public async Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(int page = 1, int pageSize = 20, string keyword = null,
            bool includeWallet = false, bool asNoTracking = false, bool isOrderByDesending = true,
            CancellationToken cancellationToken = default, TypeAdapterConfig config = default)
        {
            var query = context.Deposits.AsQueryable();

            // Search
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query
                    .Where(d => d.TraceId.Contains(keyword) ||
                    d.RefId.Contains(keyword) ||
                    d.DestinationId.Contains(keyword));
            }

            // Join to wallet.
            if (includeWallet)
            {
                query = query.Include(d => d.Wallet);
            }

            // As no tracking.
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            // Orderby
            if (isOrderByDesending)
            {
                query = query.OrderByDescending(d => d.DateTime);
            }
            else
            {
                query = query.OrderBy(d => d.DateTime);
            }

            return await query
                .PaginatedListAsync<Deposit, TDestination>(page, pageSize, cancellationToken, config);
        }

        /// <summary>
        /// Find deposit history by id.
        /// </summary>
        /// <param name="id">Deposit history id (GUID).</param>
        /// <returns>Deposit model object.</returns>
        public async Task<Deposit?> FindByIdAsync(string id, CancellationToken cancellationToken = default) => await context.Deposits.FindAsync(id, cancellationToken);

        /// <summary>
        /// Find specific deposit by traceId.
        /// </summary>
        /// <param name="traceId">Unique ref if from bank when payment request was sent.</param>
        public async Task<Deposit?> FindByTraceIdAsync(string traceId, bool includeWallet = false, CancellationToken cancellationToken = default)
        {
            var query = context.Deposits.Where(d => d.TraceId == traceId);
            if (includeWallet)
            {
                query = query.Include(d => d.Wallet);
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Find specific deposit by traceId.
        /// </summary>
        /// <param name="traceId">Unique ref if from bank when payment request was sent.</param>
        public async Task<Deposit?> FindByAuthorityAsync(string authority, bool includeWallet = false, CancellationToken cancellationToken = default)
        {
            var query = context.Deposits.Where(d => d.Authority == authority);
            if (includeWallet)
            {
                query = query.Include(d => d.Wallet);
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
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