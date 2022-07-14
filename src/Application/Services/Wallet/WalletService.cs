using DigitalWallet.Application.Dao;
using DigitalWallet.Application.Exceptions;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Repositories;

namespace DigitalWallet.Application.Services.Wallet;

public class WalletService
{
    #region Initialize & Ctor
    private readonly IWalletRepository _walletRepository;
    private readonly IUserService _userService;

    public WalletService(IWalletRepository walletRepository,
        IUserService userService)
    {
        _walletRepository = walletRepository;
        _userService = userService;
    }
    #endregion

    public async Task<Result> CreateWalletAsync(string seed, string userId, CancellationToken stoppingToken = default)
    {
        if (string.IsNullOrEmpty(userId))
        {
            throw new NotFoundException("Username cannot be null.");
        }

        var user = await _userService.FindByIdAsync(userId, stoppingToken);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        return await _walletRepository.CreateAsync(new Domain.Entities.Wallet(seed), stoppingToken);
    }
}