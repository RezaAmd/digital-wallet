using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Deposit : BaseEntity
    {
        #region Constructors
        public Deposit(double amount, string destinationId, string callback, string authority,
            string traceId = null, DepositState state = DepositState.Pending)
        {
            Id = Guid.NewGuid().ToString();
            Amount = amount;
            TraceId = traceId;
            Authority = authority;
            DateTime = DateTime.Now;
            DestinationId = destinationId;
            State = state;
            Callback = callback;
        }
        #endregion

        public string Id { get; set; }
        public string TraceId { get; set; } // Transaction id, identity, identify, refence or ...
        public string Authority { get; set; } // From bank, Track and payment in getway. (StartPay)
        public string RefId { get; set; } // Refrence id, Track id -> on success payment from bank!
        public double Amount { get; set; }
        public string Callback { get; set; }
        public DateTime DateTime { get; set; }
        public DepositState State { get; set; }

        [ForeignKey("Wallet")]
        public string DestinationId { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}