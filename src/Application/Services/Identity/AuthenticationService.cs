using Application.Interfaces;
using Application.Interfaces.Context;
using Application.Interfaces.Services;
using Application.Models;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Constructor
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IJwtService jwtService;

        public AuthenticationService(IJwtService _jwtService,
             IHttpContextAccessor _httpContext)
        {
            jwtService = _jwtService;
            httpContextAccessor = _httpContext;
        }
        #endregion

        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        public async Task SignInAsync(User user, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
            };
            #region Roles
            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }
            #endregion

            //var claimsIdentity = new ClaimsIdentity(
            //    claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(15),
                IsPersistent = isPersistent,
                IssuedUtc = DateTimeOffset.UtcNow
            };
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                authProperties);
        }

        public (Result Status, string Token) GenerateJwtToken(User user, DateTime? expire = default, List<Claim> extraClaims = null)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            if (user.UserRoles != null)
                foreach (var role in user.UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
                }
            if(extraClaims != null)
                foreach (var claim in extraClaims)
                {
                    claims.Add(claim);
                }
            //if(user.Permissions != null)
            //    foreach (var permission in user.Permissions)
            //    {
            //        claims.Add(new Claim(ClaimTypes.Role, permission.ro));
            //    }
            var jwtResult = jwtService.GenerateToken(claims, expire);
            return jwtResult;
        }

        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="authProperties">Properties applied to the login and authentication cookie.</param>
        public async Task SignInAsync(User user, AuthenticationProperties? authProperties = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
            };
            #region Roles
            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }
            #endregion

            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                authProperties);
        }

        /// <summary>
        /// Signs the current user out of the application
        /// </summary>
        public async Task SignOutAsync()
        {
            await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}