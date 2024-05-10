using DigitalWallet.Domain.Enums;
using DigitalWallet.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalWallet.Domain.Entities
{
    public class TransferEntity : BaseEntity
    {
        public string Identify { get; set; } // Its a tracking code for user.
        public Money Amount { get; set; }
        public Guid? OriginId { get; set; } = null;
        public double? OriginBalance { get; set; } = null;
        public TransferOriginType OriginType { get; set; } // 0: wallet, 1: getway
        
        [ForeignKey("Destination")]
        public Guid DestinationId { get; set; }
        public double DestinationBalance { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public TransferState State { get; set; } = TransferState.Pending;
        public string? Description { get; set; } = null;

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
            CreatedDateTime = DateTime.Now;
        }
        public TransferEntity(Money amount, double balance,
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
            CreatedDateTime = DateTime.Now;
        }
        /// <summary>
        /// For deposit transfers.
        /// </summary>
        public TransferEntity(Money amount, double balance, Guid destinationId, string description = null)
        {
            Identify = DateTime.Now.ToString("ddMMyyfffffff");
            Amount = amount;
            DestinationBalance = balance;
            DestinationId = destinationId;
            Description = description;
            CreatedDateTime = DateTime.Now;
            OriginType = TransferOriginType.Getway;
            State = TransferState.Success;
        }

        #endregion
    }
}