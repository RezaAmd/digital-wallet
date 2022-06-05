using Application.Dao;
using Application.Models;
using Application.Services.WebService.ZarinPal;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentController : ControllerBase
    {
        #region Dependency Injection
        private readonly IDepositDao _depositService;
        private readonly IZarinpalWebService _zarinpalService;
        private readonly ITransferDao _transferService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IDepositDao depositService,
            IZarinpalWebService zarinpalService,
            ITransferDao transferService,
            ILogger<PaymentController> logger)
        {
            _depositService = depositService;
            _transferService = transferService;
            _zarinpalService = zarinpalService;
            _logger = logger;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> ZarinpalCallback(string Authority, string Status, CancellationToken cancellationToken = default)
        {
            var deposit = await _depositService.FindByTraceIdAsync(Authority, includeWallet: true, cancellationToken);
            if (deposit != null)
            {
                string redirectAddress = $"{deposit.Callback}?traceId={deposit.Id}";
                // Verify request to bank.
                var verifyResult = await _zarinpalService.VerifyPaymentAsync(deposit.Amount, deposit.TraceId, cancellationToken);
                // Update deposit state.
                if (verifyResult.Response.StatusCode == HttpStatusCode.OK)
                {
                    if (verifyResult.Result.data.code == 100)
                    {
                        deposit.RefId = verifyResult.Result.data.ref_id.ToString();
                        var latestTransfer = await _transferService.GetLatestByWalletAsync(deposit.Wallet, cancellationToken);
                        var newTransfer = new Transfer(deposit.Amount, latestTransfer.Balance + deposit.Amount,
                            deposit.DestinationId, description: deposit.Id);
                        var increaseResult = await _transferService.CreateAsync(newTransfer, cancellationToken);
                        if (increaseResult.Succeeded)
                        {
                            deposit.State = DepositState.Success;
                        }
                        else
                        {
                            _logger.LogError($"Increase was failed after success deposit. Authority: {deposit.TraceId}");
                        }
                    }
                    else if (verifyResult.Result.data.code == 101)
                    {
                        deposit.State = DepositState.Successed;
                    }
                }
                else
                {
                    deposit.State = DepositState.Failed;
                }
                var depositUpdateResult = await _depositService.UpdateAsync(deposit);
                return Redirect(redirectAddress);
            }
            return NotFound("Authority not found in deposit history!");
        }
    }
}
