using DigitalWallet.Application.Dao.Identity;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Services.Identity;
using DigitalWallet.WebApi.Areas.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalWallet.WebApi.Areas.Identity.Controllers;

[ApiController]
[Area("Identity")]
[Route("[area]/[controller]/[action]")]
public class AccountController : ControllerBase
{
    #region DI
    private readonly IUserService userService;
    private readonly IAuthenticationService signInService;
    public AccountController(IAuthenticationService _signinService,
        IUserService _userService)
    {
        signInService = _signinService;
        userService = _userService;
    }
    #endregion

    [HttpPost]
    public async Task<ApiResult<object>> SignIn([FromBody] SignInDto model)
    {
        var user = await userService.FindByIdentityAsync(model.username, withPermissions: true);
        if (user != null)
        {
            var passwordValidation = userService.CheckPassword(user, model.password);
            if (passwordValidation)
            {
                var extraClaims = new List<Claim>();
                //if(user.WalletId != null)
                //{
                //    extraClaims.Add(new Claim("wallet-id", user.WalletId.Value.ToString()));
                //}
                var JwtBearer = signInService.GenerateJwtToken(user, DateTime.Now.AddHours(3), extraClaims);
                if (JwtBearer.Status.IsSuccess)
                    return Ok(new SignInVM(JwtBearer.Token));
            }
        }
        return BadRequest("نام کاربری یا رمز ورود اشتباه است.");
    }
}