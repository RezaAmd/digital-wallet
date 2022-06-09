using Application.Models;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dao
{
    public interface IDepositDao
    {
        /// <summary>
        /// Find deposit history by id.
        /// </summary>
        /// <param name="id">Deposit history id (GUID).</param>
        /// <returns>Deposit model object.</returns>
        Task<Deposit> FindByIdAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find specific deposit by traceId.
        /// </summary>
        /// <param name="traceId">Unique ref if from bank when payment request was sent.</param>
        Task<Deposit> FindByTraceIdAsync(string traceId, bool includeWallet = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find specific deposit by traceId.
        /// </summary>
        /// <param name="traceId">Unique ref if from bank when payment request was sent.</param>
        Task<Deposit?> FindByAuthorityAsync(string authority, bool includeWallet = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get deposit history by wallet id.
        /// </summary>
        /// <param name="walletId">Wallet id to fetch deposit history.</param>
        Task<PaginatedList<Deposit>> GetByWalletIdAsync(string walletId, int page = 1, int pageSize = 20,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new deposit history.
        /// </summary>
        /// <param name="deposit">New deposit model object.</param>
        Task<Result> CreateAsync(Deposit deposit, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a deposit history.
        /// </summary>
        /// <param name="deposit">Edited deposit history model object.</param>
        Task<Result> UpdateAsync(Deposit deposit, CancellationToken cancellationToken = default);
    }
}