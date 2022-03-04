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
        Task<Deposit> FindByIdAsync(string id);

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