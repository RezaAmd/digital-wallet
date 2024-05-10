using DigitalWallet.Application.Dao.Identity;
using DigitalWallet.Application.Repositories.Wallet;

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

    /// <summary>
    /// Create a new wallet with seed and owner id.
    /// </summary>
    /// <param name="seed">Seed for create wallet.</param>
    /// <param name="userId">Wallet owner id.</param>
    /// <returns>Wallet Identifier for manage.</returns>
    public async Task<Result<string>> CreateAsync(string seed, Guid userId,
        CancellationToken stoppingToken = default)
    {
        if (string.IsNullOrWhiteSpace(seed))
            return Result.Fail("Wallet seed cannot be null.");
        if (userId == Guid.Empty)
            return Result.Fail("Username cannot be null.");

        var user = await _userService.FindByIdAsync(userId, stoppingToken);
        if (user == null)
            return Result.Fail("User not found.");

        var newWallet = new WalletEntity(seed, user.Id);
        var createWalletResult = await _walletRepository.CreateAsync(newWallet, stoppingToken);
        if (createWalletResult.IsSuccess)
            return Result.Ok(newWallet.Identifier);

        return Result.Fail("Could not create new wallet.");
    }
}