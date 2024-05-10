using DigitalWallet.Application.Dao.Identity;
using DigitalWallet.Application.Exceptions;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Repositories.Wallet;
using DigitalWallet.Domain.Entities;

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

    public async Task<Result> CreateWalletAsync(string seed, Guid userId, CancellationToken stoppingToken = default)
    {
        if (userId == Guid.Empty)
        {
            throw new NotFoundException("Username cannot be null.");
        }

        var user = await _userService.FindByIdAsync(userId, stoppingToken);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        return await _walletRepository.CreateAsync(new WalletEntity(seed), stoppingToken);
    }
}