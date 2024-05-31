using DigitalWallet.Domain.ValueObjects;

namespace DigitalWallet.Domain.Entities.Identity
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; private set; } = null;
        public bool IsEmailConfirmed { get; set; } = false;
        public string PhoneNumber { get; private set; } = null;
        public bool IsPhoneNumberConfirmed { get; set; } = false;
        public PasswordHash Password { get; set; } = null;
        public Fullname Fullname { get; set; } = null;
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public bool IsBanned { get; set; } = false;

        #region Relation

        public virtual ICollection<UserRoleEntity> UserRoles { get; private set; } = null;
        public virtual ICollection<UserPermissionEntity> Permissions { get; set; } = null;
        public virtual ICollection<WalletEntity> Wallets { get; private set; } = null;

        #endregion

        #region Ctor

        UserEntity() { }
        /// <summary>
        /// Signup with email and password.
        /// </summary>
        /// <param name="email">Email address for create account</param>
        /// <param name="password">Account password for signin.</param>
        public UserEntity(string email, PasswordHash password)
        {
            if (email == null)
                throw new ArgumentNullException("Email cannot be null.");
            Email = email;

            if (password is null)
                throw new ArgumentNullException("Password cannot be null.");
            Password = password;
        }
        public UserEntity(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException("Phone number cannot be null.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set security password for signin.
        /// </summary>
        public UserEntity SetPassword(PasswordHash password)
        {
            if (password is null)
                throw new ArgumentNullException("Password cannot be null.");
            Password = password;
            return this;
        }
        /// <summary>
        /// Set phone number for user account.
        /// </summary>
        public UserEntity SetPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException("Phone number cannot be null.");
            PhoneNumber = phoneNumber;
            IsPhoneNumberConfirmed = false;
            return this;
        }
        /// <summary>
        /// Verify account phone number.
        /// </summary>
        public UserEntity ConfirmPhoneNumber()
        {
            IsPhoneNumberConfirmed = true;
            return this;
        }
        /// <summary>
        /// Set email for user account.
        /// </summary>
        /// <param name="email"></param>
        public UserEntity SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("Email cannot be null.");
            if (email.Length < 5)
                throw new ArgumentOutOfRangeException("Invalid email address.");

            Email = email.ToLower().Trim();
            IsEmailConfirmed = false;
            return this;
        }
        /// <summary>
        /// Verify account email.
        /// </summary>
        public UserEntity ConfirmEmail()
        {
            IsEmailConfirmed = true;
            return this;
        }
        /// <summary>
        /// Limit account for activity.
        /// </summary>
        public UserEntity BanAccount()
        {
            IsBanned = true;
            return this;
        }
        /// <summary>
        /// Unban account and free to activity.
        /// </summary>
        /// <returns></returns>
        public UserEntity UnbanAccount()
        {
            IsBanned = false;
            return this;
        }

        public string GetIdentityName()
            => string.IsNullOrEmpty(Email) ? PhoneNumber : Email;

        #endregion

    }
}