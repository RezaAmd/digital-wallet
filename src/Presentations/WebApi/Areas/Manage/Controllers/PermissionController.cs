using DigitalWallet.Application.Dao.Identity;
using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities.Identity;
using DigitalWallet.WebApi.Areas.Manage.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.WebApi.Areas.Manage.Controllers;

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

    [HttpGet]
    //[Authorize(Roles = "ReadPermission")]
    public async Task<ApiResult<object>> GetAll(string? keyword = null, int page = 1, CancellationToken cancellationToken = default)
    {
        int pageSize = 30;
        var permissions = await permissionService.GetAllAsync(keyword, page, pageSize, true, cancellationToken);
        return Ok(permissions);
    }

    [HttpPost]
    [ModelStateValidator]
    //[Authorize(Roles = "CreatePermission")]
    public async Task<ApiResult<object>> Create([FromBody] CreatePermissionMDto model)
    {
        var newPermission = new PermissionEntity(model.name, model.title, model.description);
        if (model.rolesId.Count > 0)
        {
            newPermission.PermissionRoles = new List<PermissionRoleEntity>();
            foreach (var roleId in model.rolesId)
            {
                newPermission.PermissionRoles.Add(new(roleId, newPermission.Id));
            }
        }
        var createResult = await permissionService.CreateAsync(newPermission);
        if (createResult.Succeeded)
            return Ok(newPermission.Id);
        return BadRequest(createResult.Errors);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "DeletePermission")]
    public async Task<ApiResult<object>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var permission = await permissionService.FindByIdAsync(id, cancellationToken);
        if (permission != null)
        {
            var deleteResult = await permissionService.DeleteAsync(permission);
            if (deleteResult.Succeeded)
                return Ok($"نقش {permission.Title} با موفقیت حذف شد.");
            return BadRequest(deleteResult.Errors);
        }
        return NotFound("نقش مورد نظر پیدا نشد.");
    }
}