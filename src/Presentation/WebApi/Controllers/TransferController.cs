﻿using Application.Dao;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TransferController : ControllerBase
    {
        #region Dependency Injection
        private readonly IWalletDao walletService;
        private readonly ITransferDao transferService;
        private readonly IDepositDao depositService;
        private readonly IUserService userService;
        public TransferController(IWalletDao _walletService,
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

        [HttpGet("{id}")]
        public async Task<ApiResult<object>> History([FromRoute] string id, CancellationToken cancellationToken = new())
        {
            var wallet = await walletService.FindByIdAsync(id);
            if (wallet != null)
            {
                var history = await transferService.GetHistoryByWalletIdAsync(wallet.Id, cancellationToken: cancellationToken);
                if (history.totalCount > 0)
                    return Ok(history);
                else
                    return NotFound(history);
            }
            return NotFound($"کیف پول { id } یافت نشد.");
        }
    }
}