using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.Identity;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private readonly IWalletDao walletService;
        private readonly ITransferService transferService;
        private readonly IDepositService depositService;
        private readonly IUserService userService;
        public WalletController(IWalletDao _walletService,
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
            string originId = User.GetCurrentWalletId(); // Current user wallet id.
            if (originId != null)
            {
                // Find origin wallet balance.
                var originLatestTransfer = await transferService.GetLatestByWalletIdAsync(originId);
                var newTransfer = new Transfer(model.Amount, 0, originId, model.WalletId, model.Description);
                var result = new IncreaseResult(newTransfer.Identify, model.Amount,
                    new PersianDateTime(newTransfer.CreatedDateTime).ToString("dddd, dd MMMM yyyy"),
                    newTransfer.State, newTransfer.Balance);
                if (originLatestTransfer != null)
                {
                    if (originLatestTransfer.Balance >= model.Amount)
                    {
                        // Get destination wallet balance.
                        var destinationLatestTransfer = await transferService.GetLatestByWalletIdAsync(model.WalletId);
                        // Create deposit history.
                        newTransfer.Balance = destinationLatestTransfer.Balance + model.Amount;
                        var transfermResult = await transferService.CreateAsync(newTransfer, cancellationToken);
                        if (transfermResult.Succeeded)
                        {
                            result.State = TransferState.Success;
                            return Ok(result);
                        }
                        return Ok(result);
                    }
                }
                return BadRequest("موجودی ناکافی میباشد.");
            }
            return NotFound("هیچ کیف پولی به شما اختصاص نشده است.");
        }

        [HttpPut]
        [ModelStateValidate]
        public async Task<ApiResult<object>> Decrease([FromBody] DecreaseDto model, CancellationToken cancellationToken = default)
        {
            // Current user wallet id.
            string destinationId = User.GetCurrentWalletId();
            if (destinationId != null)
            {
                // Find origin wallet.
                var originWallet = await walletService.FindByIdAsync(model.WalletId);
                if (originWallet != null)
                {
                    // Find destination wallet.
                    var destinationWallet = await walletService.FindByIdAsync(destinationId);
                    if (destinationWallet != null)
                    {
                        var destinationLatestTransfer = await transferService.GetLatestByWalletIdAsync(destinationId);
                        var newTransfer = new Transfer(model.Amount, destinationLatestTransfer.Balance, model.WalletId, destinationId);
                        var result = new DecreaseResult(newTransfer.Identify, model.Amount,
                            new PersianDateTime(newTransfer.CreatedDateTime).ToString("dddd, dd MMMM yyyy"),
                            newTransfer.State, newTransfer.Balance);
                        if (destinationLatestTransfer != null)
                        {
                            if (destinationLatestTransfer.Balance >= model.Amount)
                            {
                                // Get destination wallet balance.
                                var originLatestTransfer = await transferService.GetLatestByWalletIdAsync(model.WalletId);
                                // Create deposit history.
                                newTransfer.Balance = originLatestTransfer.Balance + model.Amount;
                                var transfermResult = await transferService.CreateAsync(newTransfer, cancellationToken);
                                if (transfermResult.Succeeded)
                                {
                                    result.State = TransferState.Success;
                                    result.Description = "پرداخت شما با موفقیت انجام شد.";
                                }
                            }
                            else
                            {
                                // Create new transfer.
                                await transferService.CreateAsync(newTransfer, cancellationToken);
                                result.State = TransferState.Failed;
                                result.Description = "موجودی حساب کافی نمی باشد.";
                            }
                        }
                        return Ok(result);
                    }
                }
                return NotFound("شناسه کیف پول مبدا اشتباه وارد شده.");
            }
            return NotFound("هیچ کیف پولی به شما اختصاص نشده است.");
        }
    }
}