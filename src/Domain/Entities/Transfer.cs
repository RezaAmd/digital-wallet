using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transfer : BaseEntity
    {
        #region Constructors
        public Transfer(double amount, double balance,
            string originId, string destinationId, string description = null)
        {
            Id = Guid.NewGuid().ToString();
            Amount = amount;
            Balance = balance;
            OriginId = originId;
            DestinationId = destinationId;
            Description = description;
        }
        #endregion

        public string Id { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public DateTime DateTime { get; set; }
#nullable enable
        public string? Description { get; set; }
#nullable disable
        [ForeignKey("Origin")]
        public string OriginId { get; set; }
        [ForeignKey("Destination")]
        public string DestinationId { get; set; }

        public virtual Wallet Origin { get; set; }
        public virtual Wallet Destination { get; set; }
    }
}