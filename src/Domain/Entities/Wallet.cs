using Domain.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Wallet
    {
        #region Constructors
        Wallet() { }
        public Wallet(string seed)
        {
            Id = DateTime.Now.ToString("fffffmmssHHMM");
            Seed = seed;
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
        public string? BankId { get; set; }
#nullable disable
        public virtual Bank Bank { get; set; }
        public virtual User Owner { get; set; }
    }
}