using DigitalWallet.Domain.Entities.Identity;
using DigitalWallet.Domain.ValueObjects;

namespace DigitalWallet.Domain.Entities
{
    public class SafeEntity : BaseEntity
    {
        public string Name { get; set; }
        public string ApiKey { get; private set; }
        public PasswordHash Password { get; private set; }
        public DateTime CreatedDateTime { get; private set; } = DateTime.Now;
        public Guid OwnerId { get; private set; }

        public virtual UserEntity? Owner { get; private set; } = null;
        public virtual ICollection<WalletEntity>? Wallets { get; private set; } = null;

        #region Ctor

        SafeEntity() { }
        public SafeEntity(string name, Guid ownerId)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Safe name cannot be null.");
            Name = name;

            if (ownerId == Guid.Empty)
                throw new ArgumentNullException("Safe owner id cannot be null.");
            OwnerId = ownerId;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set api key for safe.
        /// </summary>
        public SafeEntity SetApiKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("Api key cannot be null.");
            if (apiKey.Length < 10)
                throw new ArgumentOutOfRangeException("Api key cannot less than 10 character.");
            ApiKey = apiKey;
            return this;
        }

        /// <summary>
        /// Set password for safe.
        /// </summary>
        public SafeEntity SetPassword(PasswordHash password)
        {
            if (password is null
                || password.Value is null)
                throw new ArgumentNullException("Password cannot be null.");

            Password = password;
            return this;
        }

        #endregion
    }
}