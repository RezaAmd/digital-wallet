using Application.Dao;
using Application.Extentions;
using Application.Models;
using Application.Services.WebService.ZarinPal;
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
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [AllowAnonymous]
    public class WalletController : ControllerBase
    {
        #region Dependency Injection
        private readonly IWalletDao walletService;
        private readonly ITransferDao transferService;
        private readonly IDepositDao depositService;
        private readonly IUserService userService;
        private readonly IZarinPalWebService _zarinpalWebservice;

        public WalletController(IWalletDao _walletService,
            ITransferDao _transferService,
            IDepositDao _depositService,
            IUserService _userService,
            IZarinPalWebService zarinpalWebservice)
        {
            walletService = _walletService;
            transferService = _transferService;
            depositService = _depositService;
            userService = _userService;
            _zarinpalWebservice = zarinpalWebservice;
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
            return NotFound($"The wallet {id} not found.");
        }

        [HttpGet]
        public async Task<ApiResult<object>> GetBalance(CancellationToken cancellationToken = new())
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
                        // Map origin balance to result.
                        result.OriginBalance = originLatestTransfer.Balance;
                        // Check origin balance with amount.
                        if (originLatestTransfer.Balance >= model.Amount)
                        {
                            newTransfer.State = TransferState.Success;

                            #region origin balance
                            // Decrease origin balance.
                            newTransfer.OriginBalance = originLatestTransfer.Balance - model.Amount;
                            result.OriginBalance = newTransfer.OriginBalance;
                            #endregion

                            #region destination balance
                            // Find destination latest transfer (Destination balance).
                            var destinationLatestTransfer = await transferService.GetLatestByWalletAsync(wallets.second);
                            // Increase destination balance.
                            newTransfer.DestinationBalance = destinationLatestTransfer.Balance + model.Amount;
                            result.DestinationBalance = newTransfer.DestinationBalance;
                            #endregion
                        }
                        else
                        {
                            // Insufficient origin balance.
                            newTransfer.DestinationBalance = 0;
                            newTransfer.State = TransferState.InsufficientOriginBalance;
                        }
                        // Save new transfer and map the result.
                        var saveTransferResult = await transferService.CreateAsync(newTransfer, cancellationToken);
                        if (!saveTransferResult.Succeeded)
                        {
                            return BadRequest("تراکنش با خطا مواجه شد.");
                        }
                        result.State = newTransfer.State;
                        return Ok(result);
                    }
                    else
                        return NotFound($"کیف پول مقصد '{model.WalletId}' یافت نشد.");
                }
                else
                    return NotFound($"کیف پول مبدا یافت نشد.");
            }
            return NotFound("هیچ کیف پولی به شما اختصاص نشده است.");
        }

        [HttpPost]
        [ModelStateValidator]
        public async Task<ApiResult<object>> Decrease([FromBody] DecreaseDto model, CancellationToken cancellationToken = default)
        {
            string destinationId = User.GetCurrentWalletId(); // Current user wallet id.
            if (destinationId != null)
            {
                // origin wallet is first - destination wallet is second.
                var wallets = await walletService.GetTwoWalletByIdAsync(model.WalletId, destinationId, cancellationToken);
                // Validate destination wallet.
                if (wallets.second != null)
                {
                    // Validate origin wallet.
                    if (wallets.first != null)
                    {
                        // Create a new transfer.
                        var newTransfer = new Transfer(model.Amount, model.WalletId, destinationId, model.Description, state: TransferState.Failed);
                        // Create a result.
                        var result = new DecreaseResult(newTransfer.Identify, model.Amount,
                            new PersianDateTime(newTransfer.CreatedDateTime).ToString("dddd, dd MMMM yyyy"),
                            newTransfer.State);
                        // Fetch origin wallet balance.
                        var originLatestTransfer = await transferService.GetLatestByWalletAsync(wallets.first);
                        // Map origin balance to result.
                        result.OriginBalance = originLatestTransfer.Balance;
                        // Check origin balance with amount.
                        if (originLatestTransfer.Balance >= model.Amount)
                        {
                            newTransfer.State = TransferState.Success;

                            #region origin balance
                            // Decrease origin balance.
                            newTransfer.OriginBalance = originLatestTransfer.Balance - model.Amount;
                            result.OriginBalance = newTransfer.OriginBalance;
                            #endregion

                            #region destination balance
                            // Find destination latest transfer (Destination balance).
                            var destinationLatestTransfer = await transferService.GetLatestByWalletAsync(wallets.second);
                            // Increase destination balance.
                            newTransfer.DestinationBalance = destinationLatestTransfer.Balance + model.Amount;
                            result.DestinationBalance = newTransfer.DestinationBalance;
                            #endregion
                        }
                        else
                        {
                            // Insufficient origin balance.
                            newTransfer.DestinationBalance = 0;
                            newTransfer.State = TransferState.InsufficientOriginBalance;
                        }
                        // Save new transfer and map the result.
                        var saveTransferResult = await transferService.CreateAsync(newTransfer, cancellationToken);
                        if (!saveTransferResult.Succeeded)
                        {
                            return BadRequest("تراکنش با خطا مواجه شد.");
                        }
                        result.State = newTransfer.State;
                        return Ok(result);
                    }
                    else
                        return NotFound($"کیف پول مقصد '{model.WalletId}' یافت نشد.");
                }
                else
                    return NotFound($"کیف پول مبدا یافت نشد.");
            }
            return NotFound("هیچ کیف پولی به شما اختصاص نشده است.");
        }

        [HttpPost]
        [ModelStateValidator]
        public async Task<ApiResult<object>> Deposit([FromBody] DepositDto depositDto, CancellationToken cancellationToken = default)
        {
            var wallet = await walletService.FindByIdAsync(depositDto.WalletId);
            if (wallet != null)
            {
                // Payment request to bank.
                var paymentRequestResult = await _zarinpalWebservice
                    .PaymentRequestAsync(depositDto.Amount, depositDto.Description, depositDto.Mobile, depositDto.Email);
                if (paymentRequestResult.Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var newDeposit = new Deposit(depositDto.Amount, wallet.Id, paymentRequestResult.Result.data.authority);
                    // Create new deposit history.
                    var createDepositResult = await depositService.CreateAsync(newDeposit, cancellationToken);
                    if (createDepositResult.Succeeded)
                    {
                        return Ok(new DepositVM("https://www.zarinpal.com/pg/StartPay/" + paymentRequestResult.Result.data.authority));
                    }
                    else
                    {
                        return BadRequest(createDepositResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(paymentRequestResult.Response.Content);
                }
            }
            return NotFound("Wallet not found!");
        }
    }
}