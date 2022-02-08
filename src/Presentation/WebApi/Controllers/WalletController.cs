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
        public async Task<ApiResult<object>> Create([FromBody] CreateWalletDto model, CancellationToken cancellationToken = new())
        {
            Request.Headers.TryGetValue("x-bank-id", out var bankId); // Get bank id from header.

            #region Find
            // Find wallet in database.
            var wallet = await walletService.FindBySeedAsync(model.Seed, bankId);
            double balance = 0;
            if (wallet != null)
                balance = await walletService.GetBalanceAsync(wallet, cancellationToken);
            #endregion

            #region Create
            else
            {
                // Create a new wallet.
                wallet = new Wallet(model.Seed, bankId);

                var createWalletResult = await walletService.CreateAsync(wallet);
                if (!createWalletResult.Succeeded)
                    return BadRequest(createWalletResult.Errors);
            }
            #endregion

            #region fill the result
            var result = new WalletDetailVM();
            result.Id = wallet.Id;
            result.Balance = balance;
            result.CreatedDateTime = new PersianDateTime(wallet.CreatedDateTime).ToString("dddd, dd MMMM yyyy");
            #endregion

            return Ok(result);
        }

        [HttpGet]
        public async Task<ApiResult<object>> GetBalance([FromRoute] string id, CancellationToken cancellationToken)
        {
            var balance = await transferService.GetBalanceAsync(id, cancellationToken);
            return Ok(new GetBalanceVM(balance));
        }

        [HttpPost]
        public async Task<ApiResult<object>> Increase([FromBody] IncreaseDto model, CancellationToken cancellationToken = default)
        {
            string originId = User.FindFirst("wallet-id").Value;
            var transfermResult = await transferService.CreateAsync(new Transfer(model.Amount, 0, originId, model.WalletId), cancellationToken);
            if (transfermResult.Succeeded)
                return Ok(new IncreaseResult());
            return BadRequest();
        }

        [HttpPut]
        public async Task<ApiResult<object>> Decrease([FromBody] DecreaseDto model, CancellationToken cancellationToken = default)
        {
            string destinationId = "";
            var transfermResult = await transferService.CreateAsync(new Transfer(model.Amount, 0, model.WalletId, destinationId), cancellationToken);
            if (transfermResult.Succeeded)
                return Ok(new DecreaseResult());
            return BadRequest();
        }
    }
}