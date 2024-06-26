﻿using DigitalWallet.Application.Dao.Identity;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Services.Identity;
using DigitalWallet.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalWallet.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Ctor & DI

        private readonly IUserService userService;
        private readonly IAuthenticationService signInService;
        public AuthenticationController(IAuthenticationService _signinService,
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
            if (user == null)
                return BadRequest("نام کاربری یا رمز ورود اشتباه است.");
            var isUserPasswordValid = userService.CheckPassword(user, model.password);
            if (isUserPasswordValid == false)
                return BadRequest("نام کاربری یا رمز ورود اشتباه است.");
            var extraClaims = new List<Claim>();
            //if(user.WalletId != null)
            //{
            //    extraClaims.Add(new Claim("wallet-id", user.WalletId.Value.ToString()));
            //}
            var JwtBearer = signInService.GenerateJwtToken(user, DateTime.Now.AddHours(3), extraClaims);
            if (JwtBearer.Status.IsSuccess)
                return BadRequest("درخواست شما با خطا مواجه شد. لطفا دقایقی دیگر تلاش کنید.");

            return Ok(new SignInVM(JwtBearer.Token));
        }
    }
}