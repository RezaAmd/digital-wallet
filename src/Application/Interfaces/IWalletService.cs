using Application.Models;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWalletService
    {
        /// <summary>
        /// Find wallet by id.
        /// </summary>
        /// <param name="id">Wallet id</param>
        /// <returns>Wallet model object.</returns>
        Task<Wallet> FindByIdAsync(string id);

        /// <summary>
        /// Create a new wallet.
        /// </summary>
        /// <param name="wallet">New wallet model object.</param>
        Task<Result> CreateAsync(Wallet wallet, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update wallet object.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        Task<Result> UpdateAsync(Wallet wallet, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a wallet.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        Task<Result> DeleteAsync(Wallet wallet, CancellationToken cancellationToken = default);
    }
}