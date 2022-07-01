using DigitalWallet.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        #region Constructors
        Wallet() { }

        public Wallet(string seed, string bankId = null)
        {
            Id = DateTime.Now.ToString("fffffmmssHHMM");
            Seed = seed;
            if (!string.IsNullOrEmpty(bankId))
                BankId = bankId;
            CreatedDateTime = DateTime.Now;
        }
        #endregion

        public string Id { get; set; }
        public string Seed { get; set; }
        public DateTime CreatedDateTime { get; set; }

#nullable enable
        [ForeignKey("Owner")]
        public string? OwnerId { get; set; }

        [ForeignKey("Bank")]
        public string? BankId { get; set; } // Empty bank id is a sign that the wallet is public.
#nullable disable
        public virtual Bank Bank { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}