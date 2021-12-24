using Application.Extentions;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using IdentityApi.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Areas.Manage.Controllers
{
    [ApiController]
    [Route("[area]/[controller]/[action]")]
    public class WalletController : ControllerBase
    {
        #region Dependency Injection
        private readonly IWalletService walletService;
        public WalletController(IWalletService _walletService)
        {
            walletService = _walletService;
        }
        #endregion

        [HttpPost]
        [ModelStateValidate]
        public async Task<ApiResult<object>> Create([FromBody] CreateWalletDto model, CancellationToken cancellationToken = new CancellationToken())
        {
            var newWallet = new Wallet(model.seed);
            var result = await walletService.CreateAsync(newWallet, cancellationToken);
            if (result.Succeeded)
                return Ok(newWallet.Id);
            return BadRequest();
        }

        [HttpGet]
        public async Task<ApiResult<object>> GetAll(string bankId = null, int page = 1, CancellationToken cancellationToken = new CancellationToken())
        {
            int pageSize = 10;
            var wallets = await walletService.GetAllAsync(bankId, page, pageSize);
            if (wallets.totalCount > 0)
                return Ok(wallets);
            return NotFound(wallets);
        }
    }
}
