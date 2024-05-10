using DigitalWallet.Application.Dao.Identity;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Repositories.Deposit;
using DigitalWallet.Application.Repositories.Transfer;
using DigitalWallet.Application.Repositories.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class TransferController : ControllerBase
{
    #region Dependency Injection
    private readonly IWalletRepository walletService;
    private readonly ITransferRepository transferService;
    private readonly IDepositRepository depositService;
    private readonly IUserService userService;
    public TransferController(IWalletRepository _walletService,
        ITransferRepository _transferService,
        IDepositRepository _depositService,
        IUserService _userService)
    {
        walletService = _walletService;
        transferService = _transferService;
        depositService = _depositService;
        userService = _userService;
    }
    #endregion

    [HttpGet("{id}")]
    public async Task<ApiResult<object>> History([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var wallet = await walletService.FindByIdAsync(id);
        if (wallet != null)
        {
            var history = await transferService.GetHistoryByWalletIdAsync(wallet.Id, cancellationToken: cancellationToken);
            if (history.totalCount > 0)
                return Ok(history);
            else
                return NotFound(history);
        }
        return NotFound($"کیف پول {id} یافت نشد.");
    }
}