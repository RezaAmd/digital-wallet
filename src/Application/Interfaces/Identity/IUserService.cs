using Application.Models;
using Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<User> FindByIdAsync(string id);
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">New user object model.</param>
        Task<Result> CreateAsync(User user, string password, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// Update an specific user.
        /// </summary>
        /// <param name="user">Modified user you want to update.</param>
        Task<Result> UpdateAsync(User user, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// Get All Users List as Paginated List.
        /// </summary>
        /// <param name="keyword">Search to username, name, surname, fathersName and identityLetter.</param>
        /// <param name="gender">Fillter as gender.</param>
        /// <param name="tracking">For range changes.</param>
        /// <returns>List of all users</returns>
        Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(TypeAdapterConfig config = null, int page = 1, int pageSize = 20,
            bool withRoles = false, string keyword = default, bool tracking = false, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// Find user by identity (Username or Phone number or Email)
        /// </summary>
        /// <param name="identity">identity for find</param>
        Task<User> FindByIdentityAsync(string identity, bool asNoTracking = false, bool withRoles = false,
            TypeAdapterConfig config = null);

        /// <summary>
        /// Generate one time password for user phone number.
        /// </summary>
        /// <param name="phoneNumber">phone number to send otp.</param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        string GenerateOtp(string phoneNumber, HttpContext httpContext);

        /// <summary>
        /// Verify user otp.
        /// </summary>
        /// <param name="code">Otp code (one time password).</param>
        /// <returns></returns>
        (Result Result, string PhoneNumber) VerifyOtp(string code, HttpContext httpContext);

        /// <summary>
        /// Check user password is correct or not.
        /// </summary>
        /// <param name="user">User model object with password.</param>
        /// <param name="password">The password you want to check.</param>
        /// <returns>true/false</returns>
        bool CheckPassword(User user, string password);
    }
}