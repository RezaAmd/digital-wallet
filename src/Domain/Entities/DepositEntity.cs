using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.ValueObjects;
using System;

namespace DigitalWallet.Domain.Entities
{
    public class DepositEntity : BaseEntity
    {
        public string TraceId { get; set; } // Transaction id, identity, identify, refence or ...
        public string Authority { get; set; } // From bank, Track and payment in getway. (StartPay)
        public string RefId { get; set; } // Refrence id, Track id -> on success payment from bank!
        public Money Amount { get; set; }
        public string Callback { get; set; }
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public DepositState State { get; set; }
        public Guid DestinationId { get; set; }

        #region Relations

        public virtual WalletEntity Wallet { get; private set; } = null; // Destination wallet.

        #endregion

        #region Ctor

        DepositEntity() { }
        public DepositEntity(Money amount, Guid destinationId, string callback, string authority,
            string traceId = null, DepositState state = DepositState.Pending)
        {
            Amount = amount;
            TraceId = traceId;
            Authority = authority;
            DestinationId = destinationId;
            State = state;
            Callback = callback;
        }

        #endregion

    }
}