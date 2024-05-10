using DigitalWallet.Admin.WebUi.Areas.Manage.Models;
using DigitalWallet.Application.Dao.Identity;
using DigitalWallet.Domain.Entities.Identity;

namespace DigitalWallet.Admin.WebUi.Areas.Manage.Controllers;

[Area("Manage")]
[Route("[Area]/[controller]/[action]")]
public class RoleController : Controller
{
    #region Ctor & DI

    private readonly IRoleService roleService;

    public RoleController(IRoleService _roleService)
    {
        roleService = _roleService;
    }

    #endregion

    [HttpGet]
    //[Authorize(Roles = "ReadRole")]
    public async Task<ApiResult<object>> GetAll(string? keyword = null, int page = 1, CancellationToken cancellationToken = default)
    {
        int pageSize = 30;
        var roles = await roleService.GetAllAsync(keyword, page, pageSize, cancellationToken);
        if (roles.items.Count > 0)
            return Ok(roles);
        return NotFound(roles);
    }

    [HttpPost]
    [ModelStateValidator]
    //[Authorize(Roles = "CreateRole")]
    public async Task<ApiResult<object>> Create([FromBody] CreateRoleMDto model, CancellationToken cancellationToken = default)
    {
        var newRole = new RoleEntity(model.name, model.title, model.description);
        var result = await roleService.CreateAsync(newRole, cancellationToken);
        if (result.IsSuccess)
            return Ok(newRole.Id);
        return BadRequest(result.Messages);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "DeleteRole")]
    public async Task<ApiResult<object>> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var role = await roleService.FindByIdAsync(id);
        if (role != null)
        {
            var deleteResult = await roleService.DeleteAsync(role, cancellationToken);
            if (deleteResult.IsSuccess)
                return Ok($"مجوز {role.Name} با موفقیت حذف شد.");
            return BadRequest(deleteResult.Messages);
        }
        return NotFound("مجوز مورد نظر پیدا نشد.");
    }
}