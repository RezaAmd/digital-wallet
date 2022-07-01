using Application.Models;
using Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Areas.Manage.Models;

namespace WebApi.Areas.Manage.Controllers;

[Area("Manage")]
[ApiController]
[Route("[Area]/[controller]/[action]")]
public class DepositController : ControllerBase
{
    #region Dependency Injection
    private readonly ILogger<DepositController> _logger;
    private readonly IDepositRepository _depositService;

    public DepositController(IDepositRepository depositService,
        ILogger<DepositController> logger)
    {
        _depositService = depositService;
        _logger = logger;
    }
    #endregion

    [HttpGet]
    public async Task<ApiResult<object>> GetAll(int page = 1, string? keyword = null, CancellationToken cancellationToken = default)
    {
        try
        {
            int pageSize = 20;
            var deposits = await _depositService.GetAllAsync<DepositMVM>(page, pageSize, keyword,
                includeWallet: false, asNoTracking: true, isOrderByDesending: true, cancellationToken, DepositMVM.MapConfig());
            if (deposits.totalCount > 0)
            {
                return Ok(deposits);
            }
            return NotFound(deposits);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return Conflict();
    }
}