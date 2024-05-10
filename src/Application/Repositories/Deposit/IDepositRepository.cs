using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities;
using Mapster;

namespace DigitalWallet.Application.Repositories.Deposit
{
    public interface IDepositRepository
    {
        /// <summary>
        /// Get all deposits history.
        /// </summary>
        /// <param name="page">Page number. (minimum: 1)</param>
        /// <param name="pageSize">Size of page items.</param>
        /// <param name="keyword">Keyword for search. (with traceId, refId or Authority)</param>
        /// <param name="includeWallet">Join to wallet table?</param>
        /// <param name="asNoTracking">As no tracking items?</param>
        /// <returns>Paginated list of deposit entity.</returns>
        Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(int page = 1, int pageSize = 20, string? keyword = null,
            bool includeWallet = false, bool asNoTracking = false, bool isOrderByDesending = true,
            CancellationToken cancellationToken = default, TypeAdapterConfig? config = null);

        /// <summary>
        /// Find deposit history by id.
        /// </summary>
        /// <param name="id">Deposit history id (GUID).</param>
        /// <returns>Deposit model object.</returns>
        Task<DepositEntity?> FindByIdAsync(Guid id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Find specific deposit by traceId.
        /// </summary>
        /// <param name="traceId">Unique ref if from bank when payment request was sent.</param>
        Task<DepositEntity?> FindByTraceIdAsync(string traceId, bool includeWallet = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Find specific deposit by traceId.
        /// </summary>
        /// <param name="traceId">Unique ref if from bank when payment request was sent.</param>
        Task<DepositEntity?> FindByAuthorityAsync(string authority, bool includeWallet = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get deposit history by wallet id.
        /// </summary>
        /// <param name="walletId">Wallet id to fetch deposit history.</param>
        Task<PaginatedList<DepositEntity>> GetByWalletIdAsync(Guid walletId, int page = 1, int pageSize = 20,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new deposit history.
        /// </summary>
        /// <param name="deposit">New deposit model object.</param>
        Task<Result> CreateAsync(DepositEntity deposit,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a deposit history.
        /// </summary>
        /// <param name="deposit">Edited deposit history model object.</param>
        Task<Result> UpdateAsync(DepositEntity deposit,
            CancellationToken cancellationToken = default);
    }
}