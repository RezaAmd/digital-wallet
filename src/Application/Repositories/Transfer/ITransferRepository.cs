using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Repositories.Transfer
{
    public interface ITransferRepository
    {
        /// <summary>
        /// Find a transfer history by id.
        /// </summary>
        /// <param name="id">Transfer history id.</param>
        /// <returns>Transfer model object.</returns>
        Task<TransferEntity> FindByIdAsync(Guid id);

        /// <summary>
        /// Get transfer history by wallet id
        /// </summary>
        /// <param name="walletId">Wallet id</param>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Page items count.</param>
        Task<PaginatedList<TransferEntity>> GetHistoryByWalletIdAsync(Guid? walletId = default,
            DateTime startDate = default, DateTime endDate = default, int page = 1, int pageSize = 20,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get wallet balance from last transaction.
        /// </summary>
        /// <param name="walletId">Wallet id for get balance.</param>
        Task<decimal> GetBalanceByIdAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get latest transform history by wallet id.
        /// </summary>
        /// <param name="wallet">Wallet object model.</param>
        /// <returns>Transfer model object.</returns>
        Task<(TransferEntity? Transfer, decimal Balance)> GetLatestByWalletAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Find two id of latest transfer.
        /// </summary>
        /// <param name="firstId">First wallet id.</param>
        /// <param name="secondId">Second wallet id.</param>
        Task<(TransferEntity? first, TransferEntity? second)> GetTwoLatestByWalletIdAsync(Guid firstId, Guid secondId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new transfer history.
        /// </summary>
        /// <param name="transfer">New transfer history model object.</param>
        Task<Result> CreateAsync(TransferEntity transfer,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a Transfer history.
        /// </summary>
        /// <param name="transfer">Transfer model object.</param>
        Task<Result> UpdateAsync(TransferEntity transfer,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a transfer history.
        /// </summary>
        /// <param name="transfer">Transfer model object.</param>
        Task<Result> DeleteAsync(TransferEntity transfer,
            CancellationToken cancellationToken = default);
    }
}