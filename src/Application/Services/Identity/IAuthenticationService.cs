using Application.Models;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services.Identity
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        Task SignInAsync(User user, bool isPersistent = false);

        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="authProperties">Properties applied to the login and authentication cookie.</param>
        Task SignInAsync(User user, AuthenticationProperties authProperties = null);

        /// <summary>
        /// Signs the current user out of the application
        /// </summary>
        Task SignOutAsync();
        (Result Status, string Token) GenerateJwtToken(User user, DateTime? expire = default, List<Claim> extraClaims = null);
    }
}