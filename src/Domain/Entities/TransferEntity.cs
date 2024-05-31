using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.ValueObjects;

namespace DigitalWallet.Domain.Entities
{
    public class TransferEntity : BaseEntity
    {
        public string Identify { get; private set; } // Its a tracking code for user.
        public Money Amount { get; private set; }
        public TransferOriginType OriginType { get; private set; } // 0: wallet, 1: getway
        public Guid? OriginId { get; private set; } = null; // WalletId | DepositId
        public decimal? OriginBalance { get; set; } = null;

        public Guid DestinationId { get; private set; }
        public decimal DestinationBalance { get; set; }
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public TransferState State { get; set; } = TransferState.Pending;
        public string? Description { get; private set; } = null;

        public virtual WalletEntity? Destination { get; private set; } = null;

        #region Constructors

        TransferEntity() { }
        public TransferEntity(Money amount, Guid originId, Guid destinationId, string description = null,
            TransferOriginType originType = TransferOriginType.Wallet, TransferState state = TransferState.Failed)
        {
            Identify = DateTime.Now.ToString("ddMMyyfffffff");
            Amount = amount;
            OriginId = originId;
            DestinationId = destinationId;
            Description = description;
            OriginType = originType;
            State = state;
        }
        public TransferEntity(Money amount, decimal balance,
            Guid originId, Guid destinationId, string? description = null,
            TransferOriginType originType = TransferOriginType.Wallet, TransferState state = TransferState.Failed)
        {
            Identify = DateTime.Now.ToString("ddMMyyfffffff");
            Amount = amount;
            DestinationBalance = balance;
            OriginId = originId;
            DestinationId = destinationId;
            Description = description;
            OriginType = originType;
            State = state;
        }
        /// <summary>
        /// For deposit transfers.
        /// </summary>
        public TransferEntity(Money amount, decimal balance, Guid destinationId, string description = null)
        {
            Identify = DateTime.Now.ToString("ddMMyyfffffff");
            Amount = amount;
            DestinationBalance = balance;
            DestinationId = destinationId;
            Description = description;
            OriginType = TransferOriginType.Deposit;
            State = TransferState.Success;
        }

        #endregion
    }
}