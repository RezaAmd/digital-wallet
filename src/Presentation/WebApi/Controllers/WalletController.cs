using Application.Dao;
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
        private readonly ITransferDao transferService;
        private readonly IDepositDao depositService;
        private readonly IUserService userService;
        public WalletController(IWalletDao _walletService,
            ITransferDao _transferService,
            IDepositDao _depositService,
            IUserService _userService)
        {
            walletService = _walletService;
            transferService = _transferService;
            depositService = _depositService;
            userService = _userService;
        }
        #endregion

        [HttpPost]
        [ModelStateValidator]
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

        [HttpGet("{id}")]
        public async Task<ApiResult<object>> GetBalance([FromRoute] string id, CancellationToken cancellationToken = new())
        {
            var wallet = await walletService.FindByIdAsync(id);
            if (wallet != null)
            {
                var balance = await transferService.GetBalanceByIdAsync(wallet, cancellationToken);
                return Ok(new GetBalanceVM(balance));
            }
            return NotFound($"The wallet { id } not found.");
        }

        [HttpGet]
        public async Task<ApiResult<object>> GetMyBalance(CancellationToken cancellationToken = new())
        {
            var currentWalletId = User.GetCurrentWalletId();
            if (currentWalletId != null)
            {
                var currentwallet = await walletService.FindByIdAsync(currentWalletId);
                if (currentwallet != null)
                {
                    var balance = await transferService.GetBalanceByIdAsync(currentwallet, cancellationToken);
                    return Ok(new GetBalanceVM(balance));
                }
                return NotFound("کیف پول شما یافت نشد!");
            }
            return BadRequest("شما هیچ کیف پولی ندارید.");
        }

        [HttpPost]
        [ModelStateValidator]
        public async Task<ApiResult<object>> Increase([FromBody] IncreaseDto model, CancellationToken cancellationToken = default)
        {
            string originId = User.GetCurrentWalletId(); // Current user wallet id.
            if (originId != null)
            {
                // origin wallet is first - destination wallet is second.
                var wallets = await walletService.GetTwoWalletByIdAsync(originId, model.WalletId, cancellationToken);
                // Validate origin wallet.
                if (wallets.first != null)
                {
                    // Validate destination wallet.
                    if (wallets.second != null)
                    {
                        // Create a new transfer.
                        var newTransfer = new Transfer(model.Amount, originId, model.WalletId, model.Description, state: TransferState.Failed);

                        // Create a result.
                        var result = new IncreaseResult(newTransfer.Identify, model.Amount,
                            new PersianDateTime(newTransfer.CreatedDateTime).ToString("dddd, dd MMMM yyyy"),
                            newTransfer.State);

                        // Fetch origin wallet balance.
                        var originLatestTransfer = await transferService.GetLatestByWalletAsync(wallets.first);
                        double? originBalance = originLatestTransfer.transfer != null ?
                            originLatestTransfer.Balance: null;
                        // Map origin balance to result.
                        result.OriginBalance = originBalance;
                        // Check origin balance with amount.
                        if (originBalance >= model.Amount)
                        {
                            newTransfer.State = TransferState.Success;
                            #region destination transfer
                            // Find destination latest transfer (Destination balance).
                            var destinationLatestTransfer = await transferService.GetLatestByWalletAsync(wallets.second);

                            // Increase destination balance.
                            newTransfer.DestinationBalance = destinationLatestTransfer.Balance + model.Amount;
                            #endregion

                            #region origin transfer
                            // Decrease origin balance.
                            newTransfer.OriginBalance = originLatestTransfer.Balance - model.Amount;
                            #endregion
                        }
                        else
                        {
                            // Insufficient origin balance.
                            newTransfer.State = TransferState.InsufficientOriginBalance;
                        }
                        // Save new transfer and map the result.
                        var saveTransferResult = await transferService.CreateAsync(newTransfer, cancellationToken);
                        if (saveTransferResult.Succeeded)
                        {
                            newTransfer.State = TransferState.Success;
                        }
                        else
                        {
                            return BadRequest("تراکنش با خطا مواجه شد.");
                        }
                        result.State = newTransfer.State;
                        return Ok(result);
                    }
                    return NotFound($"کیف پول مقصد '{ model.WalletId }' یافت نشد.");
                }
                return NotFound($"کیف پول مبدا یافت نشد.");
            }
            return NotFound("هیچ کیف پولی به شما اختصاص نشده است.");
        }

        [HttpPost]
        [ModelStateValidator]
        public async Task<ApiResult<object>> Decrease([FromBody] DecreaseDto model, CancellationToken cancellationToken = default)
        {
            // Current user wallet id.
            string destinationId = User.GetCurrentWalletId();
            if (destinationId != null)
            {
                // Get origin and destination wallet.
                var wallets = await walletService.GetTwoWalletByIdAsync(model.WalletId, destinationId);
                if (wallets.first != null)
                {
                    // Find destination wallet.
                    if (wallets.second != null)
                    {
                        var latestTransfers = await transferService.GetTwoLatestByWalletIdAsync(model.WalletId, destinationId);

                        double destinationBalance = latestTransfers.second != null ?
                            latestTransfers.second.DestinationBalance : 0;
                        var newTransfer = new Transfer(model.Amount, latestTransfers.second.DestinationBalance, model.WalletId, destinationId);
                        var result = new DecreaseResult(newTransfer.Identify, model.Amount,
                            new PersianDateTime(newTransfer.CreatedDateTime).ToString("dddd, dd MMMM yyyy"),
                            newTransfer.State, newTransfer.DestinationBalance);
                        if (destinationBalance >= model.Amount)
                        {
                            // Get destination wallet balance.
                            double originBalance = latestTransfers.first != null ?
                                latestTransfers.first.DestinationBalance : 0;
                            // Create deposit history.
                            newTransfer.DestinationBalance = originBalance + model.Amount;
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
                        return Ok(result);
                    }
                }
                return NotFound("شناسه کیف پول مبدا اشتباه وارد شده.");
            }
            return NotFound("هیچ کیف پولی به شما اختصاص نشده است.");
        }
    }
}