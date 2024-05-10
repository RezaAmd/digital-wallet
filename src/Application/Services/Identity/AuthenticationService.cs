using DigitalWallet.Application.Interfaces.Services;
using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DigitalWallet.Application.Services.Identity;

public class AuthenticationService : IAuthenticationService
{
    #region Ctor & DI

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtService _jwtService;

    public AuthenticationService(IJwtService jwtService,
         IHttpContextAccessor httpContext)
    {
        _jwtService = jwtService;
        _httpContextAccessor = httpContext;
    }

    #endregion

    /// <summary>
    /// Signs in the specified user.
    /// </summary>
    /// <param name="user">The user to sign-in.</param>
    /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
    public async Task SignInAsync(UserEntity user, bool isPersistent = false)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.GetIdentityName()),
            };
        #region Roles

        // Add roles as claim.
        if (user.UserRoles != null)
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
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme)),
            authProperties);
    }

    public (Result Status, string Token) GenerateJwtToken(UserEntity user, DateTime? expire = default, List<Claim> extraClaims = null)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.GetIdentityName()));
        if (user.UserRoles != null)
            foreach (var role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            }
        if (extraClaims != null)
            foreach (var claim in extraClaims)
            {
                claims.Add(claim);
            }
        //if(user.Permissions != null)
        //    foreach (var permission in user.Permissions)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, permission.ro));
        //    }
        var jwtResult = _jwtService.GenerateToken(claims, expire);
        return jwtResult;
    }

    /// <summary>
    /// Signs in the specified user.
    /// </summary>
    /// <param name="user">The user to sign-in.</param>
    /// <param name="authProperties">Properties applied to the login and authentication cookie.</param>
    public async Task SignInAsync(UserEntity user, AuthenticationProperties? authProperties = null)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.GetIdentityName()),
            };
        #region Roles
        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
        }
        #endregion

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme)),
            authProperties);
    }

    /// <summary>
    /// Signs the current user out of the application
    /// </summary>
    public async Task SignOutAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}