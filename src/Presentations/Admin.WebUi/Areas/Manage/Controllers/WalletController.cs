using DigitalWallet.Admin.WebUi.Areas.Manage.Models;
using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Repositories.Wallet;
using DigitalWallet.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Admin.WebUi.Areas.Manage.Controllers;

[Area("Manage")]
[Route("[area]/[controller]/[action]")]
//[Authorize]
public class WalletController : Controller
{
    #region Ctor & DI
    private readonly IWalletRepository walletService;
    public WalletController(IWalletRepository _walletService)
    {
        walletService = _walletService;
    }
    #endregion

    [HttpPost]
    [ModelStateValidator]
    public async Task<ApiResult<object>> Create([FromBody] CreateWalletMDto model, CancellationToken cancellationToken = new CancellationToken())
    {
        Guid currentUserId = User.GetSafeMasterWalletId();
        if (currentUserId == Guid.Empty)
            return Forbid();

        var newWallet = new WalletEntity(model.seed, currentUserId);

        if (model.safeId.HasValue)
            newWallet.WithSafe(model.safeId.Value);

        var result = await walletService.CreateAsync(newWallet, cancellationToken);
        if (result.IsSuccess)
            return Ok("Wallet " + newWallet.Id + " Successfully created.");
        return BadRequest();
    }

    [HttpGet]
    public async Task<ApiResult<object>> GetAll(Guid? safeId = null, int page = 1, CancellationToken cancellationToken = new CancellationToken())
    {
        int pageSize = 10;
        var wallets = await walletService.GetAllAsync<WalletViewModelManage>(safeId, page, pageSize, cancellationToken);
        if (wallets.totalCount > 0)
            return Ok(wallets);
        return NotFound(wallets);
    }

    [HttpDelete]
    public async Task<ApiResult<object>> Delete(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
        var wallet = await walletService.FindByIdAsync(id);
        if (wallet != null)
        {
            var result = await walletService.DeleteAsync(wallet, cancellationToken);
            if (result.IsSuccess)
                return Ok("Wallet deleted successfully!");
            return BadRequest(result.Messages);
        }
        return NotFound("Wallet not found.");
    }
}