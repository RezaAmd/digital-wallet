using System;
using System.Collections.Generic;

namespace DigitalWallet.Domain.Entities
{
    public class WalletEntity : BaseEntity
    {
        #region Ctor

        WalletEntity() { }

        public WalletEntity(string seed, Guid? bankId = null)
        {
            if (seed.Length < 8)
                throw new ArgumentOutOfRangeException("Seed must be more than 8 character.");
            Seed = seed;
            BankId = bankId;
        }

        #endregion

        public string Seed { get; private set; }
        public string Identifier { get; private set; } = DateTime.Now.ToString("fffffmmssHHMM");
        public DateTime CreatedDateTime { get; private set; } = DateTime.Now;
        public Guid? BankId { get; set; } // Empty bank id is a sign that the wallet is public.
        public virtual BankEntity Bank { get; private set; } = null;
        public virtual ICollection<DepositEntity> Deposits { get; set; } = null;
    }
}