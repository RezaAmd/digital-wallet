using Application.Extentions;
using Application.Interfaces.Context;
using Application.Interfaces.Identity;
using Application.Models;
using Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Identity
{
    public class UserService : IUserService
    {
        #region Constructor
        //private readonly IUserStore<User> store;
        //private readonly IOptions<IdentityOptions> options;
        //private readonly IPasswordHasher<User> passwordHasher;
        //private readonly IEnumerable<IUserValidator<User>> userValidators;
        //private readonly IEnumerable<IPasswordValidator<User>> passwordValidators;
        //private readonly ILookupNormalizer normalizer;
        //private readonly ErrorDescriber errors;
        //private readonly IServiceProvider serviceProvider;
        //private readonly ILogger<UserManager<User>> logger;
        //private readonly IJwtService jwtService;
        private readonly string passwordEncryptionSalt = "950922";
        private readonly IDbContext context;

        public UserService(IDbContext _context)
        {
            context = _context;
        }
        #endregion

        public async Task<User> FindByIdAsync(string id)
        {
            return await context.Users.FindAsync(id);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">New user object model.</param>
        public async Task<Result> CreateAsync(User user, string password, CancellationToken cancellationToken = new CancellationToken())
        {
            if (user != null)
            {
                if (password != null)
                    user.Password = password.Encrypt(passwordEncryptionSalt);
                await context.Users.AddAsync(user);
                if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                    return Result.Success;
            }
            return Result.Failed();
        }

        /// <summary>
        /// Update an specific user.
        /// </summary>
        /// <param name="user">Modified user you want to update.</param>
        public async Task<Result> UpdateAsync(User user, CancellationToken cancellationToken = new CancellationToken())
        {
            context.Users.Update(user);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Get All Users List as Paginated List.
        /// </summary>
        /// <param name="keyword">Search to username, name, surname, fathersName and identityLetter.</param>
        /// <param name="gender">Fillter as gender.</param>
        /// <param name="tracking">For range changes.</param>
        /// <returns>List of all users</returns>
        public async Task<PaginatedList<TDestination>> GetAllAsync<TDestination>(TypeAdapterConfig config = null, int page = 1, int pageSize = 20,
            bool withRoles = false, string keyword = null, bool tracking = false, CancellationToken cancellationToken = new CancellationToken())
        {
            var init = context.Users.OrderBy(u => u.JoinedDate).AsQueryable();

            if (!tracking)
                init = init.AsNoTracking();
            // search
            if (!string.IsNullOrEmpty(keyword))
                init = init.Where(u => keyword.Contains(u.UserName) || keyword.Contains(u.Name)
                 || keyword.Contains(u.Surname) || keyword.Contains(u.Email));

            // include roles
            if (withRoles)
                init = init.Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role);

            return await init
                .ProjectToType<TDestination>(config)
                .PaginatedListAsync(page, pageSize, cancellationToken);

        }

        /// <summary>
        /// Find user by identity (Username or Phone number or Email)
        /// </summary>
        /// <param name="identity">identity for find</param>
        public async Task<User> FindByIdentityAsync(string identity, bool asNoTracking = false, bool withRoles = false,
            TypeAdapterConfig config = null)
        {
            identity = identity.ToLower();
            var init = context.Users.Where(u => u.UserName == identity
            || u.PhoneNumber == identity
            || u.Email == identity
            || u.Id == identity);
            #region include's
            if (asNoTracking)
                init = init.AsNoTracking();
            if (withRoles)
                init = init.Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role);
            #endregion
            return await init.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Check user password is correct or not.
        /// </summary>
        /// <param name="user">User model object with password.</param>
        /// <param name="password">The password you want to check.</param>
        /// <returns>true/false</returns>
        public bool CheckPassword(User user, string password)
        {
            string encryptedPassword = password.Encrypt(passwordEncryptionSalt);
            if (user.Password == encryptedPassword)
                return true;
            return false;
        }

        #region Otp and verify
        public string GenerateOtp(string phoneNumber, HttpContext httpContext)
        {
            string code = RandomGenerator.GenerateNumber();
            httpContext.Session.SetString("SigninOtp", (code + "." + phoneNumber).Encrypt());
            return code;
        }
        public (Result Result, string PhoneNumber) VerifyOtp(string code, HttpContext httpContext)
        {
            string otpSession = httpContext.Session.GetString("SigninOtp");
            if (otpSession != null)
            {
                var otpObject = otpSession.Decrypt().Split(".");
                if (otpObject[0] == code)
                {
                    httpContext.Session.Remove("SigninOtp");
                    return (Result.Success, otpObject[1]);
                }
            }
            return (Result.Failed(), null);
        }

        #endregion
    }
}