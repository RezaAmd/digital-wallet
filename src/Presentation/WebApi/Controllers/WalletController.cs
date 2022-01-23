using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.Identity;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class WalletController : ControllerBase
    {
        #region Dependency Injection
        private readonly IWalletService walletService;
        private readonly ITransferService transferService;
        private readonly IDepositService depositService;
        private readonly IUserService userService;
        public WalletController(IWalletService _walletService,
            ITransferService _transferService,
            IDepositService _depositService,
            IUserService _userService)
        {
            walletService = _walletService;
            transferService = _transferService;
            depositService = _depositService;
            userService = _userService;
        }
        #endregion

        [HttpPost]
        [ModelStateValidate]
        public async Task<ApiResult<object>> Create([FromBody] CreateWalletDto model)
        {
            var newWallet = new Wallet(model.Seed, model.BankId);
            var createWalletResult = await walletService.CreateAsync(newWallet);
            if (createWalletResult.Succeeded)
                return Ok(new NewWalletVM(newWallet.Id));
            return BadRequest();
        }

        [HttpGet]
        public async Task<ApiResult<object>> GetBalance([FromRoute] string id, CancellationToken cancellationToken)
        {
            var balance = await transferService.GetBalanceAsync(id, cancellationToken);
            return Ok(new GetBalanceVM(balance));
        }

        [HttpPut]
        public async Task<ApiResult<object>> Increase([FromBody] IncreaseDto model, CancellationToken cancellationToken = default)
        {
            string originId = "";
            var transfermResult = await transferService.CreateAsync(new Transfer(), cancellationToken);
            if (transfermResult.Succeeded)
                return Ok(new IncreaseResult());
        }

        [HttpPut]
        public async Task<ApiResult<object>> Decrease([FromBody] DecreaseDto model)
        {

            return Ok(new DecreaseResult());
        }
    }
}