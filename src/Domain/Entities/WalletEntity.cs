using DigitalWallet.Domain.Entities.Identity;
using System;
using System.Collections.Generic;

namespace DigitalWallet.Domain.Entities
{
    public class WalletEntity : BaseEntity
    {
        public string Seed { get; private set; }
        public string Identifier { get; private set; } = DateTime.Now.ToString("fffffmmssHHMM");
        public DateTime CreatedDateTime { get; private set; } = DateTime.Now;
        public Guid OwnerId { get; private set; }
        public Guid? SafeId { get; private set; } = null;

        public virtual UserEntity? Owner { get; private set; } = null;
        public virtual SafeEntity? Safe { get; private set; } = null;
        public virtual ICollection<DepositEntity>? Deposits { get; private set; } = null;


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

        #region Methods

        /// <summary>
        /// Connect the wallet to a specific safe.
        /// </summary>
        /// <param name="safeId">Safe id to connect.</param>
        public WalletEntity WithSafe(Guid safeId)
        {
            if (safeId == Guid.Empty)
                throw new ArgumentNullException("Safe id cannot be null");
            if (SafeId != null && SafeId != safeId)
                throw new ArgumentException("This wallet is connected with a safe.");
            // Connect wallet to safe.
            SafeId = safeId;
            return this;
        }

        #endregion
    }
}