using Application.Extentions;
using Application.Interfaces.Identity;
using Application.Models;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Areas.Manage.Models;

namespace WebApi.Areas.Manage.Controllers
{
    [Area("Role")]
    [ApiController]
    [Route("[Area]/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        #region Dependency Injection
        private readonly IRoleService roleService;

        public RoleController(IRoleService _roleService)
        {
            roleService = _roleService;
        }
        #endregion

        [HttpPost]
        [ModelStateValidate]
        public async Task<ApiResult<object>> CreateAsync([FromBody] CreateRoleMDto model, CancellationToken cancellationToken = new())
        {
            var newRole = new Role(model.slug, model.name, model.description);
            var result = await roleService.CreateAsync(newRole, cancellationToken);
            if (result.Succeeded)
                return Ok(newRole.Id);
            return BadRequest(result.Errors);
        }


    }
}