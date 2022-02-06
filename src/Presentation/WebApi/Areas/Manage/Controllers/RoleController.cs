using Application.Extentions;
using Application.Interfaces.Identity;
using Application.Models;
using Application.Models.Dto;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<ApiResult<object>> GetAll([FromQuery] PageParam page)
        {
            var roles = await roleService.GetAllAsync(page.page.Value, page.pageSize);
            return Ok(roles);
        }

        [HttpPost]
        [ModelStateValidate]
        //[Authorize(Roles = "CreateRole")]
        public async Task<ApiResult<object>> CreateAsync([FromBody] CreateRoleMDto model, CancellationToken cancellationToken = new())
        {
            var newRole = new Role(model.name, model.title, model.description);
            var result = await roleService.CreateAsync(newRole, cancellationToken);
            if (result.Succeeded)
                return Ok(newRole.Id);
            return BadRequest(result.Errors);
        }


    }
}