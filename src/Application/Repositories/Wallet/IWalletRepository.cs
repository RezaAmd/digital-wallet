﻿using  DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities;
using Mapster;
using System.Threading;
using System.Threading.Tasks;

namespace  DigitalWallet.Application.Repositories
{
    public interface IWalletRepository
    {
        /// <summary>
        /// Get all wallets.
        /// </summary>
        /// <param name="bankId">Bank id for get wallets of specific bank.</param>
        Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(string bankId = null, int page = 1, int pageSize = 10,
            CancellationToken cancellationToken = new CancellationToken(), TypeAdapterConfig config = default);

        /// <summary>
        /// Find a specific wallet with seed.
        /// </summary>
        /// <param name="seed">Wallet seed value.</param>
        /// <param name="bankId">specific bank id.</param>
        /// <returns>Wallet model object.</returns>
        Task<Wallet> FindBySeedAsync(string seed, string bankId = null, CancellationToken cancellationToken = default);

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

        /// <summary>
        /// Get a specific wallet balance.
        /// </summary>
        /// <param name="wallet">Wallet model object.</param>
        /// <returns>Wallet balance.</returns>
        Task<double> GetBalanceAsync(Wallet wallet, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get two wallet by id.
        /// </summary>
        /// <param name="first">First wallet id.</param>
        /// <param name="second">Second wallet id.</param>
        Task<(Wallet first, Wallet second)> GetTwoWalletByIdAsync(string first, string second, CancellationToken cancellationToken = new());
    }
}