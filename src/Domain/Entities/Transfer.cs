using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transfer : BaseEntity
    {
        #region Constructors
        Transfer() { }

        public Transfer(double amount, string originId, string destinationId,
            string description = null, TransferOriginType originType = TransferOriginType.Wallet, TransferState state = TransferState.Failed)
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

        public Transfer(double amount, double balance,
            string originId, string destinationId,
            string description = null, TransferOriginType originType = TransferOriginType.Wallet, TransferState state = TransferState.Failed)
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
        #endregion

        public string Id { get; set; }
        public string Identify { get; set; } // Its a tracking code for user.
        public double Amount { get; set; }
        public double DestinationBalance { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string OriginId { get; set; } // 0: wallet, 1: getway
        public TransferOriginType OriginType { get; set; }
        public TransferState State { get; set; }
#nullable enable
        public double? OriginBalance { get; set; }
        public string? Description { get; set; }
#nullable disable

        [ForeignKey("Destination")]
        public string DestinationId { get; set; }

        public virtual Wallet Destination { get; set; }
    }
}