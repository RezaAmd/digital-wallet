using Application.Interfaces.Identity;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Areas.Identity.Models;

namespace WebApi.Areas.Identity.Controllers
{
    [ApiController]
    [Area("Identity")]
    [Route("[area]/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        #region DI
        private readonly IUserService userService;
        private readonly ISignInService signInService;
        public AccountController(ISignInService _signinService,
            IUserService _userService)
        {
            signInService = _signinService;
            userService = _userService;
        }
        #endregion

        [HttpPost]
        public async Task<ApiResult<object>> SignIn([FromBody] SignInDto model)
        {
            var user = await userService.FindByIdentityAsync(model.username);
            if (user != null)
            {
                var passwordValidation = userService.CheckPassword(user, model.password);
                if (passwordValidation)
                {
                    var JwtBearer = signInService.GenerateJwtToken(user, DateTime.Now.AddMinutes(10));
                    if (JwtBearer.Status.Succeeded)
                        return Ok(new SignInVM(JwtBearer.Token));
                }
            }
            return BadRequest();
        }
    }
}