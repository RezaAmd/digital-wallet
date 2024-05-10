using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities;
using Mapster;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Repositories.Wallet
{
    public interface IWalletRepository
    {
        /// <summary>
        /// Get all wallets.
        /// </summary>
        /// <param name="bankId">Bank id for get wallets of specific bank.</param>
        Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(Guid? bankId = null, int page = 1, int pageSize = 10,
            CancellationToken cancellationToken = default, TypeAdapterConfig? config = null);

        /// <summary>
        /// Find a specific wallet with seed.
        /// </summary>
        /// <param name="seed">Wallet seed value.</param>
        /// <param name="bankId">specific bank id.</param>
        /// <returns>Wallet model object.</returns>
        Task<WalletEntity?> FindBySeedAsync(string seed, Guid? bankId = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Find wallet by id.
        /// </summary>
        /// <param name="id">Wallet id</param>
        /// <returns>Wallet model object.</returns>
        Task<WalletEntity?> FindByIdAsync(Guid id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new wallet.
        /// </summary>
        /// <param name="wallet">New wallet model object.</param>
        Task<Result> CreateAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Update wallet object.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        Task<Result> UpdateAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a wallet.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        Task<Result> DeleteAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a specific wallet balance.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        /// <returns>Wallet balance.</returns>
        Task<double> GetBalanceAsync(WalletEntity wallet,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get two wallet by id.
        /// </summary>
        /// <param name="first">First wallet id.</param>
        /// <param name="second">Second wallet id.</param>
        Task<(WalletEntity? first, WalletEntity? second)> GetTwoWalletByIdAsync(Guid first, Guid second,
            CancellationToken cancellationToken = default);
    }
}