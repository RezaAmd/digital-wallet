using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities
{
    public class DepositEntity : BaseEntity
    {
        #region Constructors
        DepositEntity() { }

        public DepositEntity(Money amount, Guid destinationId, string callback, string authority,
            string traceId = null, DepositState state = DepositState.Pending)
        {
            Amount = amount;
            TraceId = traceId;
            Authority = authority;
            DateTime = DateTime.Now;
            DestinationId = destinationId;
            State = state;
            Callback = callback;
        }
        #endregion

        public string TraceId { get; set; } // Transaction id, identity, identify, refence or ...
        public string Authority { get; set; } // From bank, Track and payment in getway. (StartPay)
        public string RefId { get; set; } // Refrence id, Track id -> on success payment from bank!
        public Money Amount { get; set; }
        public string Callback { get; set; }
        public DateTime DateTime { get; set; }
        public DepositState State { get; set; }

        public Guid DestinationId { get; set; }
        public virtual WalletEntity? Wallet { get; set; } // Destination wallet.
    }
}