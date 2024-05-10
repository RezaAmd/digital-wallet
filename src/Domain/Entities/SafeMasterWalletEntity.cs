using System;

namespace DigitalWallet.Domain.Entities
{
    public class SafeMasterWalletEntity : BaseEntity
    {
        public Guid WalletId { get; private set; }
        public Guid SafeId { get; private set; }

        public virtual WalletEntity? Wallet { get; private set; } = null;
        public virtual SafeEntity? Safe { get; private set; } = null;

        #region Ctor

        SafeMasterWalletEntity() { }
        public SafeMasterWalletEntity(Guid walletId, Guid safeId)
        {
            if (walletId == Guid.Empty)
                throw new ArgumentOutOfRangeException("Wallet id cannot be empty.");
            WalletId = walletId;

            if (safeId == Guid.Empty)
                throw new ArgumentOutOfRangeException("Safe id cannot be empty.");
            SafeId = safeId;
        }

        #endregion
    }
}