using Application.Interfaces;
using Application.Interfaces.Identity;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("[controller]/[action]")]
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
        public async Task<ApiResult<object>> Create([FromBody] CreateWalletDto model)
        {

            return Ok(new NewWalletVM("WALLET_ID"));
        }

        [HttpGet]
        public async Task<ApiResult<object>> GetBalance([FromRoute] string id, CancellationToken cancellationToken)
        {
            var balance = await transferService.GetBalanceAsync(id, cancellationToken);
            return Ok(new GetBalanceVM(balance));
        }

        [HttpPut]
        public async Task<ApiResult<object>> Increase([FromBody] IncreaseDto model)
        {

            return Ok(new IncreaseResult());
        }

        [HttpPut]
        public async Task<ApiResult<object>> Decrease([FromBody] DecreaseDto model)
        {

            return Ok(new DecreaseResult());
        }
    }
}