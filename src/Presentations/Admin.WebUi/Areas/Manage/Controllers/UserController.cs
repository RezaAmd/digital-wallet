using DigitalWallet.Admin.WebUi.Areas.Manage.Models;
using DigitalWallet.Application.Dao.Identity;
using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Repositories.Wallet;
using DigitalWallet.Domain.Entities.Identity;
using DigitalWallet.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Admin.WebUi.Areas.Manage.Controllers;

[Area("Manage")]
[Route("[area]/[controller]/[action]")]
//[Authorize]
public class UserController : Controller
{
    #region Ctor & DI
    private readonly IUserService userService;
    private readonly IWalletRepository walletService;
    private readonly IPermissionService permissionService;
    private readonly ILogger<UserController> logger;

    public UserController(IUserService _userService,
        IWalletRepository _walletService,
        IPermissionService _permissionService,
        ILogger<UserController> _logger)
    {
        userService = _userService;
        walletService = _walletService;
        logger = _logger;
        permissionService = _permissionService;
    }
    #endregion

    [HttpGet]
    public async Task<ApiResult<object>> GetAll(string? keyword = null, int page = 1, CancellationToken cancellationToken = default)
    {
        int pageSize = 20;
        var users = await userService.GetAllAsync<UserThumbailMVM>(keyword: keyword, page: page, pageSize: pageSize, cancellationToken: cancellationToken);
        if (users.totalCount > 0)
            return Ok(users);
        return NotFound(users);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<object>> Get([FromRoute] Guid id)
    {
        var user = await userService.FindByIdAsync(id);
        if (user is null)
            return NotFound("کاربر مورد نظر یافت نشد.");

        return Ok(new UserThumbailMVM(user.Id, user.PhoneNumber, user.Email, user.Fullname!.Name, user.Fullname!.Surname));
    }

    [HttpPost]
    [ModelStateValidator]
    //[Authorize(Roles = "CreateUser")]
    public async Task<ApiResult<object>> Create([FromBody] CreateUserMDto model, CancellationToken cancellationToken = new CancellationToken())
    {
        // Create new user.
        var newUser = new UserEntity(model.email, new PasswordHash(model.password))
            .SetPhoneNumber(model.phoneNumber);

        if (!string.IsNullOrEmpty(model.name) || !string.IsNullOrEmpty(model.surname))
            newUser.Fullname = new Fullname(model.name, model.surname);

        var createUserResult = await userService.CreateAsync(newUser, model.password, cancellationToken);
        if (createUserResult.IsSuccess == false)
        {
            logger.LogError("Failed to create new user.");
            return BadRequest(createUserResult.Messages);
        }

        return Ok(new CreateUserMVM(newUser.Id));
    }

    [HttpPost("{id}")]
    //[Authorize(Roles = "UpdateUser")]
    public async Task<ApiResult<object>> Edit([FromRoute] Guid id, [FromBody] EditUserMDto model, CancellationToken cancellationToken)
    {
        var user = await userService.FindByIdAsync(id);
        if (user != null)
        {
            bool hasChanged = false;

            #region Validate

            if (model.phoneNumber.ToLower() != user.PhoneNumber.ToLower())
            {
                hasChanged = true;
                user.SetPhoneNumber(model.phoneNumber);
            }

            if (model.email.ToLower() != user.Email.ToLower())
            {
                hasChanged = true;
                user.SetEmail(model.email);
            }

            if (model.name.ToLower() != user.Fullname!.Name!.ToLower() ||
                model.surname.ToLower() != user.Fullname!.Surname!.ToLower())
            {
                hasChanged = true;
                user.Fullname = new Fullname(model.name, model.surname);
            }

            #endregion

            if (hasChanged)
            {
                var updateResult = await userService.UpdateAsync(user, cancellationToken);
                if (updateResult.IsSuccess)
                    return Ok();
                return BadRequest(updateResult.Messages);
            }
            return BadRequest("هیچ تغییراتی صورت نگرفته است!");
        }
        return NotFound("کاربر مورد نظر پیدا نشد.");
    }

    //[HttpDelete("{id}")]
    //[Authorize(Roles = "DeleteUser")]
    //public async Task<ApiResult<object>> Delete([FromRoute] Guid id,
    //    CancellationToken cancellationToken)
    //{
    //    var user = await userService.FindByIdAsync(id);
    //    if (user is null)
    //        return NotFound("کاربر مورد نظر پیدا نشد.");

    //    if (user.WalletId.HasValue)
    //    {
    //        var wallet = await walletService.FindByIdAsync(user.WalletId.Value);
    //        if (wallet is not null)
    //        {
    //            var walletDeleteResult = await walletService.DeleteAsync(wallet, cancellationToken);
    //        }
    //    }

    //    var userDeleteResult = await userService.DeleteAsync(user, cancellationToken);
    //    if (userDeleteResult.IsSuccess == false)
    //        return BadRequest(userDeleteResult.Messages);

    //    return Ok("کاربر " + user.Username + " با موفقیت حذف گردید.");
    //}

    [HttpGet]
    public async Task<ApiResult<object>> AssignPermission(Guid userId, Guid permissionId,
        CancellationToken cancellationToken)
    {
        var user = await userService.FindByIdAsync(userId);
        if (user is null)
            return NotFound("کاربر مورد نظر پیدا نشد.");

        var permission = await permissionService.FindByIdAsync(permissionId, cancellationToken);
        if (permission is null)
            return NotFound("نقش مورد نظر پیدا نشد.");

        var assignResult = await userService.AddToPermissionAsync(user, permission);
        if (assignResult.IsSuccess == false)
            return BadRequest(assignResult.Messages);

        return Ok($"نقش {permission.Title} با موفقیت به کاربر {user.GetIdentityName()} اختصاص داده شد.");
    }
}