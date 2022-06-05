using Application.Dao;
using Application.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentController : ControllerBase
    {
        #region Dependency Injection
        private readonly IDepositDao _depositService;

        public PaymentController(IDepositDao depositService)
        {
            _depositService = depositService;
        }
        #endregion

        [HttpGet]
        public async Task<ApiResult<object>> ZarinpalCallback(string Authority, string Status)
        {
            var deposit = await _depositService.FindByTraceIdAsync(Authority);
            if (deposit != null)
            {
                // TODO: Verify request to bank.

                // TODO: Update deposit state.
                deposit.State = DepositState.Success;
                var depositUpdateResult = await _depositService.UpdateAsync(deposit);
                if (depositUpdateResult.Succeeded)
                {

                    return Ok();
                }
                return BadRequest();
            }
            return NotFound("Authority not found in deposit history!");
        }
    }
}
