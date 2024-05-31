using DigitalWallet.Domain.Entities.Identity;
using DigitalWallet.Domain.ValueObjects;

namespace DigitalWallet.Domain.Entities
{
    public class WalletEntity : BaseEntity
    {
        public string Seed { get; private set; }
        public string Identifier { get; private set; } = DateTime.Now.ToString("fffffmmssHHMM");
        public string ApiKey { get; private set; }
        public PasswordHash Password { get; private set; }
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public Guid OwnerId { get; private set; }
        public Guid? MasterWalletId { get; set; }

        #region Relations

        public virtual UserEntity Owner { get; private set; } = null;
        public virtual ICollection<DepositEntity> Deposits { get; private set; } = null;
        public virtual WalletEntity MasterWallet { get; private set; } = null;
        public virtual ICollection<WalletEntity> SubWallets { get; set; } = null;

        #endregion

        #region Ctor

        WalletEntity() { }
        public WalletEntity(string seed, Guid ownerId)
        {
            if (seed.Length < 8)
                throw new ArgumentOutOfRangeException("Seed must be more than 8 character.");
            Seed = seed;

            if (ownerId == Guid.Empty)
                throw new ArgumentNullException("Wallet Owner id cannot be null.");
            OwnerId = ownerId;
        }

        #endregion
    }
}