using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Deposit : BaseEntity
    {
        #region Constructors
        public Deposit(double amount, string destinationId, DepositState state = DepositState.Pending, string traceId = null)
        {
            Id = Guid.NewGuid().ToString();
            Amount = amount;
            DateTime = DateTime.Now;
            State = state;
            TraceId = traceId;
            DestinationId = destinationId;
        }
        #endregion

        public string Id { get; set; }
        public string TraceId { get; set; } // Transaction id, identity, identify, refence or ...
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public DepositState State { get; set; }

        [ForeignKey("Wallet")]
        public string DestinationId { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}