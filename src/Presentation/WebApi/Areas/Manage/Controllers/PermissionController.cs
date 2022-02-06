using Application.Interfaces.Identity;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Areas.Manage.Controllers
{
    [ApiController]
    [Area("Manage")]
    [Route("[Area]/[controller]/[action]")]
    public class PermissionController : ControllerBase
    {
        #region Dependency Injection
        private readonly IPermissionService permissionService;

        public PermissionController(IPermissionService _permissionService)
        {
            permissionService = _permissionService;
        }
        #endregion

        [HttpPost]
        public async Task<ApiResult<object>> Create()
        {
            return Ok();
        }
    }
}