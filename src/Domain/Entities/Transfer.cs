using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transfer : BaseEntity
    {
        #region Constructors
        Transfer() { }

        public Transfer(Money amount, string originId, string destinationId, string description = null,
            TransferOriginType originType = TransferOriginType.Wallet, TransferState state = TransferState.Failed)
        {
            Id = Guid.NewGuid().ToString();
            Identify = DateTime.Now.ToString("ddMMyyfffffff");
            Amount = amount;
            OriginId = originId;
            DestinationId = destinationId;
            Description = description;
            OriginType = originType;
            State = state;
            CreatedDateTime = DateTime.Now;
        }

        public Transfer(Money amount, double balance,
            string originId, string destinationId, string description = null,
            TransferOriginType originType = TransferOriginType.Wallet, TransferState state = TransferState.Failed)
        {
            Id = Guid.NewGuid().ToString();
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
        public Transfer(Money amount, double balance, string destinationId, string description = null)
        {
            Id = Guid.NewGuid().ToString();
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

        public string Id { get; set; }
        public string Identify { get; set; } // Its a tracking code for user.
        public Money Amount { get; set; }
        public string? OriginId { get; set; }
#nullable enable
        public double? OriginBalance { get; set; }
#nullable disable
        public TransferOriginType OriginType { get; set; } // 0: wallet, 1: getway
        
        [ForeignKey("Destination")]
        public string DestinationId { get; set; }
        public double DestinationBalance { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public TransferState State { get; set; }
#nullable enable
        public string? Description { get; set; }
#nullable disable


        public virtual Wallet Destination { get; set; }
    }
}