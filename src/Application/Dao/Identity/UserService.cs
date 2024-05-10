using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Interfaces.Context;
using DigitalWallet.Application.Models;
using DigitalWallet.Domain.Entities.Identity;
using DigitalWallet.Domain.Enums;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Application.Dao.Identity
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

        public async Task<UserEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Users
                .Include(x => x.Permissions)
                .Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">New user object model.</param>
        public async Task<Result> CreateAsync(UserEntity user, string password, CancellationToken cancellationToken = new CancellationToken())
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
        public async Task<Result> UpdateAsync(UserEntity user, CancellationToken cancellationToken = new CancellationToken())
        {
            context.Users.Update(user);
            if (Convert.ToBoolean(await context.SaveChangesAsync(cancellationToken)))
                return Result.Success;
            return Result.Failed();
        }

        /// <summary>
        /// Delete a specific user.
        /// </summary>
        /// <param name="user">User model object.</param>
        public async Task<Result> DeleteAsync(UserEntity user, CancellationToken cancellationToken = new CancellationToken())
        {
            context.Users.Remove(user);
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
            bool withRoles = false, string? keyword = null, bool tracking = false, CancellationToken cancellationToken = new CancellationToken())
        {
            var init = context.Users.OrderBy(u => u.JoinedDate).AsQueryable();

            if (!tracking)
                init = init.AsNoTracking();
            // search
            if (!string.IsNullOrEmpty(keyword))
                init = init.Where(u => keyword.Contains(u.Username) || keyword.Contains(u.Name)
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
        public async Task<UserEntity?> FindByIdentityAsync(string identity, bool asNoTracking = false, bool withRoles = false,
            bool withPermissions = false,
            TypeAdapterConfig? config = null)
        {
            if (string.IsNullOrEmpty(identity))
                return null;
            identity = identity.ToLower();
            var query = context.Users.Where(u => u.Username == identity
            || u.PhoneNumber == identity
            || u.Email == identity).AsQueryable();

            var id = Guid.Empty;
            Guid.TryParse(identity, out id);
            if (id != Guid.Empty)
                query = query.Where(u => u.Id == id);

            #region include's
            if (asNoTracking)
                query = query.AsNoTracking();
            if (withRoles)
                query = query.Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role);
            if (withPermissions)
                query = query.Include(u => u.Permissions)
                    .ThenInclude(up => up.Permission);
            #endregion

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Check user password is correct or not.
        /// </summary>
        /// <param name="user">User model object with password.</param>
        /// <param name="password">The password you want to check.</param>
        /// <returns>true/false</returns>
        public bool CheckPassword(UserEntity user, string password)
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


        #region Permission
        public async Task<Result> AddToPermissionAsync(UserEntity user, PermissionEntity permission,
            RelatedPermissionType type = RelatedPermissionType.General)
        {
            user.Permissions.Add(new(user.Id, permission.Id, type));
            return await UpdateAsync(user);
        }
        #endregion
    }
}