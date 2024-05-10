using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Repositories.Wallet;
using DigitalWallet.Domain.Entities;
using DigitalWallet.WebApi.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.WebApi.Areas.Manage.Controllers;

[ApiController]
[Area("Manage")]
[Route("[area]/[controller]/[action]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WalletController : ControllerBase
{
    #region Dependency Injection
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
        var newWallet = new WalletEntity(model.seed, model.safeId);
        var result = await walletService.CreateAsync(newWallet, cancellationToken);
        if (result.Succeeded)
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
            if (result.Succeeded)
                return Ok("Wallet deleted successfully!");
            return BadRequest(result.Errors);
        }
        return NotFound("Wallet not found.");
    }
}